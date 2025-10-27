namespace Rent
{
    public class BachelorPad
    {
        public object Id { get; set; }
        public byte[] Image { get; set; }
        public int Price { get; set; }
        public string Link { get; set; }
        public string Address { get; set; }
        public int Quality { get; set; }
        public int Location { get; set; }
        public int Aesthetics { get; set; }
        public int Furniture { get; set; }
        public string Phone { get; set; }
        public string Observation { get; set; }
        public string ChatLink { get; set; }
        public override string ToString() => Address ?? Price.ToString();
    }
}
