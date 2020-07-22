using Focus.Incident.Domain.Incident.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Person.Models
{
    public class Person : EntityBase
    {
        //public int Id { get; set; }
        public string Name { get; set; } //Assigned to column in DB
    }
}
