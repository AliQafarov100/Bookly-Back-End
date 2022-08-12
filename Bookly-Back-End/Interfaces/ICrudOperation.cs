using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Interfaces
{
    public interface ICrudOperation
    {
       IEnumerable<Team> Teams { get; }
        public void Create(Team team);
        public void Update(Team team, int id);
    }
}
