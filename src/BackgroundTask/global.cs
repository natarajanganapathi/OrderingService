global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Hosting;
global using System.IO;
global using Azure.Messaging.ServiceBus;
global using System.Text.Json;
global using Domain.DomainModel.OrderDomainModel.Entity;

global using Infrastructure;
global using Infrastructure.DatabaseContext;
global using Infrastructure.Repositories;

global using BackgroundTasks.Tasks;

