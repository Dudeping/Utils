using System;
using System.Collections.Generic;
using System.Linq;
using Codeping.Agile.Core;

namespace Codeping.Utils.ExcelMapper
{
    public class ModelMetadata
    {
        public ModelMetadata(Type type)
        {
            this.Type = type;

            var properties = (this.IsTupleType = type.IsGenericType && type.IsTupleType())
                ? type.GetGenericArguments().SelectMany(x => x.GetProperties())
                : type.GetProperties();

            int index = 0;

            this.Properties = properties.Select(prop => new PropertyMetadata(index++, prop)).ToList();
        }

        public PropertyMetadata IndexOf(ColumnMetadata column, MapingMethod method)
        {
            switch (method)
            {
                case MapingMethod.PropertyName:
                    return this.Properties.FirstOrDefault(x => x.ProperyName == column.Name);

                case MapingMethod.DisplayName:
                    return this.Properties.FirstOrDefault(x => x.DisplayName == column.Name);

                case MapingMethod.IndexAttribute:
                    return this.Properties.FirstOrDefault(x => x.PropertyIndex == column.Index);
            }

            return null;
        }

        public Type Type { get; }
        public bool IsTupleType { get; }
        public IEnumerable<PropertyMetadata> Properties { get; }
    }
}
