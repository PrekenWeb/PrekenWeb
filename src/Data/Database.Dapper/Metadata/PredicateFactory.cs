using System;
using System.Collections.Generic;
using DapperExtensions;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class PredicateFactory : IPredicateFactory
    {
        private readonly Dictionary<Type, List<FilterMetadata>> _metadataByFilter = new Dictionary<Type, List<FilterMetadata>>();

        public PredicateFactory(IEnumerable<IFilterMetadataProvider> metadataProviders)
        {
            foreach (var metadataProvider in metadataProviders)
            {
                if (metadataProvider.Metadata == null || metadataProvider.Metadata.Count == 0)
                    continue;

                if (_metadataByFilter.ContainsKey(metadataProvider.Type))
                    _metadataByFilter[metadataProvider.Type].AddRange(metadataProvider.Metadata);
                else
                    _metadataByFilter.Add(metadataProvider.Type, metadataProvider.Metadata);
            }
        }

        public IPredicate GetPredicate<TFilter, TData>(TFilter filter) where TFilter : DataFilter<TFilter, TData> where TData : class
        {
            if (filter == null)
                return null;

            List<FilterMetadata> metadataForFilter;
            if (!_metadataByFilter.TryGetValue(typeof(TFilter), out metadataForFilter))
                return null;

            var predicatesGroup = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            foreach (var untypedMetadata in metadataForFilter)
            {
                var metadata = (FilterMetadata<TFilter, TData>)untypedMetadata;

                var filterValue = metadata.FilterValue(filter);
                if (filterValue != null && filterValue != metadata.DefaultValue)
                    predicatesGroup.Predicates.Add(Predicates.Field(metadata.FilterExpression, metadata.FilterType, filterValue));
            }

            return predicatesGroup.Predicates.Count > 0
                ? predicatesGroup
                : null;
        }
    }
}