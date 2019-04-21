using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace Codeping.Utils.ExcelMapper
{
    public class Reader
    {
        private readonly Settings _settings;
        private readonly ModelSetter _setter;
        private readonly ModelMetadata _metadata;

        private IDictionary<int, ColumnMetadata> _header;

        public Reader(
            Settings settings,
            ModelSetter setter,
            ModelMetadata metadata)
        {
            _setter = setter;
            _settings = settings;
            _metadata = metadata;
        }

        public IEnumerable<object> Read(ISheet sheet)
        {
            this.ReadHeader(sheet);

            int start = _settings.HasHeader ? 1 : 0;

            var models = new List<object>();

            for (int i = start; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                var values = new Dictionary<Type, object>();

                for (int j = 0; j < row.LastCellNum; j++)
                {
                    if (_header.TryGetValue(j, out var column) &&
                        _metadata.IndexOf(column, _settings.MapingMethod) is PropertyMetadata meta)
                    {
                        var type = meta.PropertyInfo.DeclaringType;

                        values.TryGetValue(type, out var value);

                        if (value == null)
                        {
                            value = type.CreateInstance<object>();

                            values.Add(type, value);
                        }

                        var cell = row.GetCell(j);

                        if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                        {
                            _setter.SetValue(value, meta.Index, cell.DateCellValue);
                        }
                        else
                        {
                            _setter.SetValue(value, meta.Index, cell.ToString());
                        }
                    }
                }

                if (_metadata.IsTupleType)
                {
                    var args = _metadata.Type.GenericTypeArguments.Select(arg =>
                    {
                        values.TryGetValue(arg, out object value);

                        return value ?? arg.CreateInstance<object>();
                    });

                    var value = _metadata.Type.CreateInstance<object>(args.ToArray());

                    models.Add(value);
                }
                else
                {
                    if (values.Count > 0)
                    {
                        models.Add(values.Values.First());
                    }
                }
            }

            return models;
        }

        private void ReadHeader(ISheet sheet)
        {
            _header = new Dictionary<int, ColumnMetadata>();

            IRow header = sheet.GetRow(0);

            for (int i = 0; i < header.LastCellNum; i++)
            {
                var name = _settings.HasHeader
                    ? header.GetCell(i).StringCellValue
                    : string.Empty;

                _header.Add(i, new ColumnMetadata(i, name));
            }
        }
    }
}
