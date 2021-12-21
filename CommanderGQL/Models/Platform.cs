using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    [GraphQLDescription("Represents any software or service that has commands")]
    public class Platform
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }

        [GraphQLDescription("Valid license key for the platform")]
        public string LicenseKey { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();   // a platform can have 0 or more commands
    }
}
