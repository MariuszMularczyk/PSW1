using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBPSW
{

    class Animal : TableEntity
    {

		public string Name { get; set; }
		public int BirthYear { get; set; }
		public string Comments { get; set; }
		public int Catwalk { get; set; }

		public string Species { get; set; }
		public string  Breed { get; set; }


		
		public Animal()
		{
			PartitionKey = "animals";
			RowKey = Guid.NewGuid().ToString();
		}

		override public string ToString()
		{
			return "Zwierze " + Name + " urodzone w " + BirthYear + ", Uwagi: " + Comments + ", nr wybiegu: " + Catwalk  + ", gatunek: " + Species + ", rasa: " + Breed;
		}
	}
}
