﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands;

public class DeleteProductByIdCommand : IRequest<bool>
{
    public required string Id { get; init; }
}
