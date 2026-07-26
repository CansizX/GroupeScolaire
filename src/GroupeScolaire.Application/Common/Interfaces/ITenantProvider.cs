using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeScolaire.Application.Common.Interfaces;

public interface ITenantProvider
{
    Guid? TenantId { get; }
    string? ConnectionString { get; }
}
