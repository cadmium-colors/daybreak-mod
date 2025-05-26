using System;
using System.Diagnostics;
using System.IO;

using Build.Shared;

namespace Build.Pre.Features.Shaders;

internal sealed class ShaderCompiler : BuildTask
{
    private static string BaseDirectory => Path.GetDirectoryName(Path.GetFullPath(Environment.ProcessPath!))!;

    public override void Run(ProjectContext ctx)
    {
        var fxcExe = Path.Combine(BaseDirectory, "native", "fxc.exe");

        if (!File.Exists(fxcExe))
        {
            Console.Error.WriteLine($"error SHADERC: fxc.exe not found (expected at '{fxcExe}')");
            Environment.ExitCode = 1;
            return;
        }

        var fxcExePath = "";
        if (OperatingSystem.IsLinux())
        {
            var otherProcess = new Process();
            var processStartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "/bin/bash",
                Arguments = "-c \"command -v wine\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            otherProcess.StartInfo = processStartInfo;
            otherProcess.Start();

            var error = otherProcess.StandardError.ReadToEnd();
            var output = otherProcess.StandardOutput.ReadToEnd();

            if (!string.IsNullOrEmpty(error))
            {
                Console.Error.WriteLine("error SHADERC: " + error);
                Environment.ExitCode = 1;
                return;
            }
            if (!string.IsNullOrEmpty(output))
            {
                fxcExePath = fxcExe;
                fxcExe = output.Trim();
            }
            else
            {
                Console.Error.WriteLine("error SHADERC: WINE not found; maybe try installing it from your package manager?");
                Environment.ExitCode = 1;
                return;
            }
        }

        foreach (var (_, fullPath) in ctx.EnumerateGroup("shaders"))
        {
            CompileShader(fxcExePath, fxcExe, fullPath);
        }
    }

    private static void CompileShader(string fxcExePath, string fxcExe, string filePath)
    {
        var fxcOutput = Path.ChangeExtension(filePath, ".fxc");

        var pInfo = new ProcessStartInfo
        {
            FileName = fxcExe,
            Arguments = $"{fxcExePath} /T fx_2_0 \"{filePath}\" /Fo \"{fxcOutput}\" /D FX=1 /O3 /Op",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        Console.WriteLine(fxcExe);
        Console.WriteLine(pInfo.Arguments);

        using var process = new Process();
        process.StartInfo = pInfo;
        process.OutputDataReceived += (_, e) => PrintInfo(e.Data, filePath);
        process.ErrorDataReceived += (_, e) => PrintError(e.Data, filePath);

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            return;
        }

        Console.Error.WriteLine($"{filePath}: error SHADERC: fxc.exe exited with code {process.ExitCode}");
        Environment.ExitCode = process.ExitCode;
    }

    private static void PrintInfo(string? message, string filePath)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        Console.WriteLine(message);
    }

    private static void PrintError(string? message, string filePath)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        Console.Error.WriteLine($"{filePath}: error SHADERC: {message}");
    }
}