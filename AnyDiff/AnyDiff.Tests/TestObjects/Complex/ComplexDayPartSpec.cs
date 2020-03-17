using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AnyDiff.Tests.TestObjects.Complex
{
    public class ComplexDayPartSpec : IRequiresPostInitializationSetup
    {
        public int Id { get; set; }
        public int BuySpecificationDayPartId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string WeekdayStartTime { get; set; }
        public string WeekdayEndTime { get; set; }
        public string SaturdayStartTime { get; set; }
        public string SaturdayEndTime { get; set; }
        public string SundayStartTime { get; set; }
        public string SundayEndTime { get; set; }

        public decimal? AllocationPercent { get; set; }

        public int ProgramFilterType { get; set; }

        public decimal ProgramFilterValue { get; set; }

        public void PostInitializationSetup()
        {
            SetUnderlyingCostTableId();
        }

        private void SetUnderlyingCostTableId()
        {
            if (_costTableId != null)
            {
                Budgets?.ForEach((x, i) => x.CostTableId = _costTableId);
            }
        }

        private int? _costTableId;

        public int? CostTableId
        {
            get => Budgets?.FirstOrDefault()?.CostTableId;
            set => _costTableId = value;
        }

        public int MaxPerWeek { get; set; }

        public int? MaxSpotLimitPerWeekOther1h2h { get; set; }

        public int? MaxSpotLimitPerWeekOther2h3h { get; set; }

        public int? MaxSpotLimitPerWeekOther3hPlus { get; set; }

        public int? MaxSpotLimitPerWeekOther30m1h { get; set; }

        public decimal MaxLocalCableAllocation { get; set; }

        public decimal MinLocalCableAllocation { get; set; }

        public bool IsAttachedToBuyline { get; set; }

        public int LocalCableAllocationNumericType { get; set; } = (int)ITNNumericInputType.Impression;

        public ComplexDataDayPartValues DayPartFlexibility { get; set; } = new ComplexDataDayPartValues();

        public IList<ComplexDataSpecBudget> Budgets { get; set; } = new List<ComplexDataSpecBudget>();

        public IList<ComplexDataSpecRule> Rules { get; set; } = new List<ComplexDataSpecRule>();

        public IList<int> ExcludedProgramIds { get; set; } = new List<int>();

        public IList<int> TimezoneIds { get; set; } = new List<int>();

        public IList<int> ExcludedStationOrganizationIds { get; set; } = new List<int>();

        public ComplexDataSpecWeeklyValues WeeklyFlexibility { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ComplexDayPartSpec;

            if (other == null)
            {
                return false;
            }

            return Id == other.Id && Name == other.Name && Code == other.Code
                && WeekdayStartTime == other.WeekdayStartTime && WeekdayEndTime == other.WeekdayEndTime
                && SaturdayStartTime == other.SaturdayStartTime && SaturdayEndTime == other.SaturdayEndTime
                && SundayStartTime == other.SundayStartTime && SundayEndTime == other.SundayEndTime
                && AllocationPercent == other.AllocationPercent;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (WeekdayStartTime != null ? WeekdayStartTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (WeekdayEndTime != null ? WeekdayEndTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SaturdayStartTime != null ? SaturdayStartTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SaturdayEndTime != null ? SaturdayEndTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SundayStartTime != null ? SundayStartTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SundayEndTime != null ? SundayEndTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AllocationPercent != null ? AllocationPercent.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public enum ITNNumericInputType
    {
        Impression = 1,
        Cost = 2,
        Grp = 3,
        Cpp = 4,
        Cpm = 5,
    }

    public class ComplexDataSpecRule
    {
        public enum ViewType
        {
            Cost,
            Imp
        }

        public enum SpecificityType
        {
            Market,
            All
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int[] DaysOfWeekIds { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CategoryId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SpecificityType Specificity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ViewType View { get; set; }
        public int[] MarketIds { get; set; }
        public int[] ProgramIds { get; set; }
        public int[] StationOrganizationIds { get; set; }
        public int? DaypartId { get; set; }

        public ComplexDataSpecRule()
        {
            Specificity = SpecificityType.All;
            View = ViewType.Cost;
            MarketIds = new int[0];
            ProgramIds = new int[0];
            StationOrganizationIds = new int[0];
            DaysOfWeekIds = new int[0];
        }
    }

    public class ComplexDataSpecBudget
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Quarter { get; set; }
        public int MarketId { get; set; }

        public bool HasMarketAttachedToDeal { get; set; }

        public bool AutoDistribute { get; set; }

        public int? CostTableId { get; set; }

        public int? DigitalAllocationMin { get; set; }

        public int? DigitalAllocationMax { get; set; }

        public IList<ComplexDataBudgetAllocation> SpotLengthWeeks { get; set; } = new List<ComplexDataBudgetAllocation>();
                
        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object o)
        {
            var other = o as ComplexDataSpecBudget;

            if (other == null)
            {
                return false;
            }

            var match = Year == other.Year && string.Equals(Quarter, other.Quarter)
                        && MarketId == other.MarketId;

            if (match)
            {
                if (SpotLengthWeeks.Count != other.SpotLengthWeeks.Count)
                {
                    return false;
                }

                var spotLengthWeeksMatch = SpotLengthWeeks.All(other.SpotLengthWeeks.Contains);
                if (!spotLengthWeeksMatch)
                    return false;
            }

            return match;
        }
    }

    public class ComplexDataBudgetAllocation
    {
        public int SpotLengthId { get; set; }

        public decimal? CPM { get; set; }

        public decimal? CPP { get; set; }

        public IList<ComplexDataSpecDemo> AlternateDemographicCosts { get; set; } = new List<ComplexDataSpecDemo>();

        public IList<ComplexDataSpecWeek> Weeks { get; set; } = new List<ComplexDataSpecWeek>();

        public override int GetHashCode()
        {
            return SpotLengthId;
        }

        public override bool Equals(object o)
        {
            var other = o as ComplexDataBudgetAllocation;

            if (other == null)
            {
                return false;
            }

            if (Weeks.Count != other.Weeks.Count)
            {
                return false;
            }

            if (AlternateDemographicCosts.Count != other.AlternateDemographicCosts.Count)
            {
                return false;
            }

            var weeksMatch = Weeks.All(other.Weeks.Contains);

            if (!weeksMatch)
            {
                return false;
            }

            var demoMatch = AlternateDemographicCosts.All(other.AlternateDemographicCosts.Contains);

            if (!demoMatch)
            {
                return false;
            }

            return SpotLengthId == other.SpotLengthId && CPM == other.CPM && CPP == other.CPP;
        }
    }

    public class ComplexDataSpecWeek
    {
        public DateTime Week { get; set; }
        public double Value { get; set; }

        public ICollection<int> DaysOfWeek { get; set; } = new HashSet<int>();

        public override int GetHashCode()
        {
            return Week.GetHashCode();
        }

        public override bool Equals(object o)
        {
            var other = o as ComplexDataSpecWeek;

            if (other == null)
            {
                return false;
            }

            return Week == other.Week && Value == other.Value &&
                DaysOfWeek.OrderBy(x => x).SequenceEqual(other.DaysOfWeek.OrderBy(z => z));
        }
    }

    public class ComplexDataSpecDemo
    {
        public int DemographicId { get; set; }
        public decimal? CPP { get; set; }
        public decimal? CPM { get; set; }

        public override int GetHashCode()
        {
            return DemographicId;
        }

        public override bool Equals(object o)
        {
            var other = o as ComplexDataSpecDemo;

            if (other == null)
            {
                return false;
            }

            return
                DemographicId == other.DemographicId &&
                CPP == other.CPP &&
                CPM == other.CPM;
        }
    }

    public class ComplexDataSpecWeeklyValues
    {
        public decimal ValuePercent { get; set; }

        [JsonProperty(ItemConverterType = typeof(DateFormatConverter), ItemConverterParameters = new object[] { "yyyy-MM-dd" })]
        public DateTime[] ExcludedWeeks { get; set; }
    }

    public class ComplexDataDayPartValues
    {
        public decimal ValuePercent { get; set; }
        public IList<int> DayPartIds { get; set; } = new List<int>();
    }

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }

    public interface IRequiresPostInitializationSetup
    {
        void PostInitializationSetup();
    }
}
