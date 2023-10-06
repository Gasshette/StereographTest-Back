using System;
using System.Collections.Generic;

namespace Stereograph.TechnicalTest.Api.Entities;

public partial class Project
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Comment { get; set; }

    public string Step { get; set; }
}
