﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Contracts
{
    public interface ILieutenantGeneral:IPrivate
    {
       IReadOnlyCollection<IPrivate> Privates { get; }
    }
}
