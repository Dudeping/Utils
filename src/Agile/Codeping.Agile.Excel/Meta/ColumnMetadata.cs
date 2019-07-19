namespace Codeping.Agile.Excel
{
    public class ColumnMetadata
    {
        public ColumnMetadata(int index)
            : this(index, string.Empty)
        {
        }

        public ColumnMetadata(int index, string name)
        {
            this.Index = index;
            this.Name = name;

        }

        public int Index { get; set; }

        public string Name { get; set; }
    }
}
