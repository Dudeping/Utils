using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Codeping.Utils.ExcelMapper
{
    public partial class ExcelReader : IDisposable
    {
        private readonly IWorkbook _workbook;

        public ExcelReader(string fullPath)
            : this(new FileStream(fullPath, FileMode.Open, FileAccess.Read))
        {
        }

        public ExcelReader(Stream steam)
        {
            try
            {
                _workbook = new XSSFWorkbook(steam);
            }
            catch (Exception)
            {
                _workbook = new HSSFWorkbook(steam);
            }
        }

        public ExcelReader(IWorkbook workbook)
        {
            _workbook = workbook;
        }

        public IEnumerable<T> Read<T>(Settings settings)
        {
            return this.Read<T>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2>> Read<T1, T2>(Settings settings)
        {
            return this.Read<Tuple<T1, T2>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3>> Read<T1, T2, T3>(Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4>> Read<T1, T2, T3, T4>(Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5>> Read<T1, T2, T3, T4, T5>(Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Read<T1, T2, T3, T4, T5, T6>(Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Read<T1, T2, T3, T4, T5, T6, T7>(Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6, T7>>(_workbook.GetSheetAt(0), settings);
        }

        public IEnumerable<T> Read<T>(int sheet, Settings settings)
        {
            return this.Read<T>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2>> Read<T1, T2>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3>> Read<T1, T2, T3>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4>> Read<T1, T2, T3, T4>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5>> Read<T1, T2, T3, T4, T5>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Read<T1, T2, T3, T4, T5, T6>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Read<T1, T2, T3, T4, T5, T6, T7>(int sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6, T7>>(_workbook.GetSheetAt(sheet), settings);
        }

        public IEnumerable<T> Read<T>(string sheet, Settings settings)
        {
            return this.Read<T>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2>> Read<T1, T2>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2>>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3>> Read<T1, T2, T3>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3>>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4>> Read<T1, T2, T3, T4>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4>>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5>> Read<T1, T2, T3, T4, T5>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5>>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Read<T1, T2, T3, T4, T5, T6>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6>>(_workbook.GetSheet(sheet), settings);
        }

        public IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Read<T1, T2, T3, T4, T5, T6, T7>(string sheet, Settings settings)
        {
            return this.Read<Tuple<T1, T2, T3, T4, T5, T6, T7>>(_workbook.GetSheet(sheet), settings);
        }

        private IEnumerable<TResult> Read<TResult>(ISheet sheet, Settings settings)
        {
            if (sheet == null)
            {
                return Enumerable.Empty<TResult>();
            }

            var type = typeof(TResult);

            var modelMetadata = new ModelMetadata(type);

            var modelSetter = new ModelSetter(modelMetadata);

            var reader = new Reader(settings, modelSetter, modelMetadata);

            return reader.Read(sheet).Select(x => x.Cast<TResult>()).ToList();
        }

        public void Dispose()
        {
            _workbook.Dispose();
        }
    }
}
