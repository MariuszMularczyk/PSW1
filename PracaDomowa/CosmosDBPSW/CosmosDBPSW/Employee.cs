using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBPSW
{
    class Employee : TableEntity
	{

		public string Name { get; set; }
		public int BirthYear { get; set; }
		public string Specialization { get; set; }
		public double Salary { get; set; }

		public Employee()
		{
			PartitionKey = "employees";
		}

		override public string ToString()
		{
			return "Employee " + Name + " born in " + BirthYear + ", specialization:  " + Specialization + ", Salary: " + Salary;
		}
	}
}
