namespace ExtraCredit {
	public class CustomerRecord {
		public int Id;
		public string Name;
		public float Owed;
		
		public CustomerRecord(int id, string name, float owed) {
			Id = id;
			Name = name;
			Owed = owed;
		}
	}
}