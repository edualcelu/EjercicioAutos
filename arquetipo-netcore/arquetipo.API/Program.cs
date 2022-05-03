using Microsoft.EntityFrameworkCore;
using arquetipo.Entity.Models;
using arquetipo.Repository.Context;

using arquetipo.Domain.Interfaces;
using arquetipo.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using arquetipo.API;

var app = Startup.InizializarApp(args);
app.Run();
