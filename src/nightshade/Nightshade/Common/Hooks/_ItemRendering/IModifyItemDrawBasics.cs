using Daybreak.Common.Features.Hooks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

using Hook = Nightshade.Common.Hooks._ItemRendering.IModifyItemDrawBasics;

namespace Nightshade.Common.Hooks._ItemRendering;

public interface IModifyItemDrawBasics
{
    public static readonly GlobalHookList<GlobalItem> HOOK = ItemLoader.AddModHook(
        GlobalHookList<GlobalItem>.Create(x => ((Hook)x).ModifyItemDrawBasics)
    );

    void ModifyItemDrawBasics(Item item, int slot, ref Texture2D texture, ref Rectangle frame, ref Rectangle glowmaskFrame);

    public static void Invoke(Item item, int slot, ref Texture2D texture, ref Rectangle frame, ref Rectangle glowmaskFrame)
    {
        foreach (var g in HOOK.Enumerate(item))
        {
            if (g is not Hook hook)
            {
                continue;
            }

            hook.ModifyItemDrawBasics(item, slot, ref texture, ref frame, ref glowmaskFrame);
        }
    }

    [OnLoad]
    private static void Load()
    {
        On_Main.DrawItem_GetBasics += (On_Main.orig_DrawItem_GetBasics orig, Main self, Item item, int slot, out Texture2D texture, out Rectangle frame, out Rectangle glowmaskFrame) =>
        {
            orig(self, item, slot, out texture, out frame, out glowmaskFrame);
            Invoke(item, slot, ref texture, ref frame, ref glowmaskFrame);
        };
    }
}