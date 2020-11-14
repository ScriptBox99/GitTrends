﻿using GitTrends.Mobile.Common;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using static GitTrends.MarkupExtensions;
using static Xamarin.Forms.Markup.GridRowsColumns;

namespace GitTrends
{
    class StarsStatisticsGrid : Grid
    {
        public StarsStatisticsGrid()
        {
            this.FillExpand()
                .DynamicResource(BackgroundColorProperty, nameof(BaseTheme.CardStarsStatsIconColor));

            Padding = 20;
            RowSpacing = 8;

            RowDefinitions = Rows.Define(
                (Row.TopLine, AbsoluteGridLength(1)),
                (Row.Total, StarGridLength(1)),
                (Row.Stars, StarGridLength(3)),
                (Row.Message, StarGridLength(1)),
                (Row.BottomLine, AbsoluteGridLength(1)));

            ColumnDefinitions = Columns.Define(
                (Column.LeftStar, StarGridLength(1)),
                (Column.Text, StarGridLength(1)),
                (Column.RightStar, StarGridLength(1)));

            Children.Add(new SeparatorLine()
                            .Row(Row.TopLine).ColumnSpan(All<Column>()));

            Children.Add(new StarsStatisticsLabel("TOTAL", 24)
                            .Row(Row.Total).ColumnSpan(All<Column>()));

            Children.Add(new StarSvg()
                            .Row(Row.Stars).Column(Column.LeftStar));

            Children.Add(new StarsStatisticsLabel(48) { AutomationId = TrendsPageAutomationIds.StarsStatisticsLabel }
                            .Row(Row.Stars).Column(Column.Text)
                            .Bind<Label, double, string>(Label.TextProperty, nameof(TrendsViewModel.TotalStars), convert: totalStars => totalStars.ToAbbreviatedText()));

            Children.Add(new StarSvg()
                            .Row(Row.Stars).Column(Column.RightStar));

            Children.Add(new StarsStatisticsLabel("KEEP IT UP!", 24)
                            .Row(Row.Message).ColumnSpan(All<Column>()));

            Children.Add(new SeparatorLine()
                            .Row(Row.BottomLine).ColumnSpan(All<Column>()));
        }

        enum Row { TopLine, Total, Stars, Message, BottomLine }
        enum Column { LeftStar, Text, RightStar }

        class StarSvg : SvgImage
        {
            public StarSvg()
                : base("star.svg", () => Color.White, 44, 44)
            {
                this.Center();
            }
        }

        class SeparatorLine : BoxView
        {
            public SeparatorLine() => BackgroundColor = Color.White;
        }

        class StarsStatisticsLabel : Label
        {
            public StarsStatisticsLabel(in string text, in int fontSize) : this(fontSize)
            {
                Text = text;
            }

            public StarsStatisticsLabel(in int fontSize)
            {
                this.TextCenter();

                FontSize = fontSize;
                TextColor = Color.FromHex(DarkTheme.PageBackgroundColorHex);
                FontFamily = FontFamilyConstants.RobotoBold;
            }
        }
    }
}
