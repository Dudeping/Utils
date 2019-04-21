using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeping.Utils.ExcelMapper
{
    public class ModelSetter
    {
        private readonly IDictionary<int, Action<object, object>> _setters;

        public ModelSetter(ModelMetadata metadata)
        {
            _setters = metadata.Properties.ToDictionary(x => x.Index, x =>
            {
                return new Action<object, object>((model, value) =>
                {
                    value = value.ChangeType(x.PropertyInfo.PropertyType);

                    x.PropertyInfo.SetValue(model, value);
                });
            });
        }


        public void SetValue(object model, int index, object value)
        {
            if (_setters.TryGetValue(index, out var setter))
            {
                setter(model, value);
            }
        }
    }
}
