using JetBrains.Annotations;

using Microsoft.Xna.Framework.Graphics;

using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;

namespace Daybreak.Common.Features.ModPanel;

/// <summary>
///     An API extension to <see cref="ModPanelStyle"/> that enables you to
///     directly interface with the <see cref="UIModItem"/> instance rather than
///     the diluted <see cref="UIPanel"/> instance.
///     <br />
///     Intended for use by developers with assembly publicizers who need
///     convenient access to the object.
/// </summary>
[PublicAPI]
[Autoload(Side = ModSide.Client)]
public abstract class ModPanelStyleExt : ModPanelStyle
{
    /// <inheritdoc cref="ModPanelStyle.PreInitialize"/>
    public sealed override bool PreInitialize(UIPanel element)
    {
        return PreInitialize((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.PostInitialize"/>
    public sealed override void PostInitialize(UIPanel element)
    {
        PostInitialize((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModIcon"/>
    public sealed override UIImage? ModifyModIcon(UIPanel element, UIImage modIcon, ref int modIconAdjust)
    {
        return ModifyModIcon((UIModItem)element, modIcon, ref modIconAdjust);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModName"/>
    public sealed override UIText ModifyModName(UIPanel element, UIText modName)
    {
        return ModifyModName((UIModItem)element, modName);
    }

    /// <inheritdoc cref="ModPanelStyle.PreSetHoverColors"/>
    public sealed override bool PreSetHoverColors(UIPanel element, bool hovered)
    {
        return PreSetHoverColors((UIModItem)element, hovered);
    }

    /// <inheritdoc cref="ModPanelStyle.PostSetHoverColors"/>
    public sealed override void PostSetHoverColors(UIPanel element, bool hovered)
    {
        PostSetHoverColors((UIModItem)element, hovered);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDraw"/>
    public sealed override bool PreDraw(UIPanel element, SpriteBatch sb)
    {
        return PreDraw((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDraw"/>
    public sealed override void PostDraw(UIPanel element, SpriteBatch sb)
    {
        PostDraw((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawPanel"/>
    public sealed override bool PreDrawPanel(UIPanel element, SpriteBatch sb)
    {
        return PreDrawPanel((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawPanel"/>
    public sealed override void PostDrawPanel(UIPanel element, SpriteBatch sb)
    {
        PostDrawPanel((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PreInitialize"/>
    public virtual bool PreInitialize(UIModItem element)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostInitialize"/>
    public virtual void PostInitialize(UIModItem element) { }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    /// <inheritdoc cref="ModPanelStyle.ModifyModIcon"/>
    public virtual UIImage? ModifyModIcon(UIModItem element, UIImage modIcon, ref int modIconAdjust)
    {
        return modIcon;
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModName"/>
    public virtual UIText ModifyModName(UIModItem element, UIText modName)
    {
        return modName;
    }

    /// <inheritdoc cref="ModPanelStyle.PreSetHoverColors"/>
    public virtual bool PreSetHoverColors(UIModItem element, bool hovered)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostSetHoverColors"/>
    public virtual void PostSetHoverColors(UIModItem element, bool hovered) { }

    /// <inheritdoc cref="ModPanelStyle.PreDraw"/>
    public virtual bool PreDraw(UIModItem element, SpriteBatch sb)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDraw"/>
    public virtual void PostDraw(UIModItem element, SpriteBatch sb) { }

    /// <inheritdoc cref="ModPanelStyle.PreDrawPanel"/>
    public virtual bool PreDrawPanel(UIModItem element, SpriteBatch sb)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawPanel"/>
    public virtual void PostDrawPanel(UIModItem element, SpriteBatch sb) { }
}