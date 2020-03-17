using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace AnyDiff.Tests.TestObjects.Complex
{
    public class ComplexDataSpec : ComplexDataSpecMinimal, IRequiresPostInitializationSetup
    {
        public decimal TotalBrandBudget { get; set; }

        public decimal TotalGRP { get; set; }

        public int AirtimeSeparationId { get; set; }

        public decimal MaxSharePerStation { get; set; }

        public decimal MinSharePerStation { get; set; }

        public int? MaxPerWeek { get; set; }

        public int? MaxSpotLimitPerWeekOther1h2h { get; set; }

        public int? MaxSpotLimitPerWeekOther2h3h { get; set; }

        public int? MaxSpotLimitPerWeekOther3hPlus { get; set; }

        public int? MaxSpotLimitPerWeekOther30m1h { get; set; }

        public int? DigitalAllocationMin { get; set; }

        public int? DigitalAllocationMax { get; set; }

        public decimal MaxLocalCableAllocation { get; set; }

        public decimal MinLocalCableAllocation { get; set; }

        public int DistributorOrganizationId { get; set; }

        public bool HasDealAttached { get; set; }

        public int LocalCableAllocationNumericType { get; set; } = (int)ITNNumericInputType.Impression;

        public IList<ComplexDayPartSpec> DayParts { get; set; } = new List<ComplexDayPartSpec>();

        public IList<ComplexDataSpecRule> Rules { get; set; } = new List<ComplexDataSpecRule>();

        public ComplexDataSpecWeeklyValues WeeklyFlexibility { get; set; }

        [JsonIgnore]
        public ICollection<ComplexDataSpecBudget> Budgets
        {
            get {
                return DayParts
                    .SelectMany(x => x.Budgets)
                    .ToList();
            }
        }

        [OnDeserialized]
        private void OnDeserializingMethod(StreamingContext context) => PostInitializationSetup();

        public void PostInitializationSetup()
        {
            DayParts?.ForEach((x, i) => x.PostInitializationSetup());
        }
    }

    public class ComplexDataSpecMinimal : ComplexDataSpecSubMinimal
    {
        public bool IsSent { get; set; }

        public int ParentBuySpecificationId { get; set; }

        public bool IsClone { get; set; }

        public bool IsOutOfDate { get; set; }

        public ComplexDataSpecMinimal()
        {
            MarketsIds = new List<int>();
            ExcludedProgramIds = new List<int>();
            ExcludedProgramCategoryIds = new List<int>();
            ExcludedStationOrganizationIds = new List<int>();
            AlternateDemographicIds = new List<int>();
        }
    }

    public class ComplexDataSpecSubMinimal : AuditableBase, IId<int>
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioName { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int AdvertiserOrganizationId { get; set; }

        public int BrandOrganizationId { get; set; }

        public int[] MediaTypeIds { get; set; }

        public int MarketTypeId { get; set; }

        public int DemographicValueTypeId { get; set; }

        public int GoalsMetricTypeId { get; set; }

        public IList<int> MarketsIds { get; set; }

        public IList<int> ExcludedProgramIds { get; set; }

        public IList<int> ExcludedProgramCategoryIds { get; set; }

        public IList<int> ExcludedStationOrganizationIds { get; set; }

        public int PrimaryDemographicId { get; set; }

        public IList<int> AlternateDemographicIds { get; set; }

        public Guid ChangesetItemHash { get; set; }

        public int RatingTypeId { get; set; }
    }

    public abstract class AuditableBase : IUIAuditable
    {
        public string CreatedByName { get; set; }

        public string ModifiedByName { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }
    }

    public interface IUIAuditable : IUIAuditableDateTime, IUIAuditableName
    {

    }

    public interface IUIAuditableDateTime
    {
        DateTime DateCreatedUtc { get; set; }

        DateTime DateModifiedUtc { get; set; }
    }

    public interface IUIAuditableName
    {
        string CreatedByName { get; set; }

        string ModifiedByName { get; set; }
    }

    public interface IId<T> where T : struct
    {
        T Id { get; set; }
    }
}
