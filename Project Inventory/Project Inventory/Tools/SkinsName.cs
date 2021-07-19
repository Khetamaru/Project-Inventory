using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Inventory.Tools
{
    /// <summary>
    /// Library of names of all type of positions used.
    /// </summary>
    public enum SkinName

    {
        Standart,
        StandartLittleMargin,

        None
    }

    public enum SkinLocation
    {
        TopLeft,
        StretchLeft,
        BottomLeft,
        TopStretch,
        BottomStretch,
        StretchStretch,
        CenterCenter,
        StretchCenter,
        CenterStretch,
        TopRight,
        StretchRight,
        BottomRight,
        TopCenter,
        CenterLeft,
        CenterRight,
        BottomCenter,

        None
    }

    public enum SkinSize
    {
        WidthOneTier,
        WidthTwoTier,
        HeightOneTier,
        HeightTwoTier,
        HeightTenPercent,
        HeightEightPercent,
        HeightNintyPercent,
        HeightFifteenPercent,
        HeightTwentyPercent,

        None
    }
}
