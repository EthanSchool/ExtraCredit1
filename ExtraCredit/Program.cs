using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ExtraCredit {
	internal static class Program {

		private static List<CustomerRecord> _records;

		private static void Main() {
			_records = File.Exists("Records.json") ? JsonConvert.DeserializeObject<List<CustomerRecord>>(File.ReadAllText("Records.json")) : new List<CustomerRecord>();
			
			ConsoleKeyInfo key;
			do {
				Console.WriteLine("Press <1> to add record, <2> to query records, and <Esc> to exit");
				key = Console.ReadKey(true);
				switch(key.KeyChar) {
					case '1':
						PromptForRecord();
						break;
					case '2':
						QueryForRecord();
						break;
				}
			} while(key.Key != ConsoleKey.Escape);
			
			File.WriteAllText("Records.json", JsonConvert.SerializeObject(_records));
		}

		private static void QueryForRecord() {
			Console.WriteLine("Enter minimum amount owed for query");
			float owed = PromptFloat("Enter balance owed");
			List<CustomerRecord> records = _records.Where(i => i.Owed >= owed).ToList();

			Console.WriteLine($"Results: ({records.Count})");
			foreach(CustomerRecord record in records) {
				Console.WriteLine($"Id: {record.Id}, Name: {record.Name}, Owed: {record.Owed}");
			}
		}

		private static void PromptForRecord() {
			Console.WriteLine("Enter informatoon for new Customer Record");
			int id = PromptInt("Enter ID numer");
			string name = PromptString("Enter Name");
			float owed = PromptFloat("Enter balance owed");
			_records.Add(new CustomerRecord(id, name, owed));
		}

		private static int PromptInt(string message) {
			while(true) {
				Console.Write(message + ": ");
				if(int.TryParse(Console.ReadLine(), out int value)) return value;
				Console.Write("Error: Invalid input value");
			}
		}
		
		private static float PromptFloat(string message) {
			while(true) {
				Console.Write(message + ": ");
				if(float.TryParse(Console.ReadLine(), out float value)) return value;
				Console.Write("Error: Invalid input value");
			}
		}
		
		private static string PromptString(string message) {
			while(true) {
				Console.Write(message + ": ");
				string value = Console.ReadLine();
				if(value != "") return value;
				Console.Write("Error: Invalid input value");
			}
		}
	}
}