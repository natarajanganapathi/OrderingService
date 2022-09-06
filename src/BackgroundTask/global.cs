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
global using System.Threading;
global using System.Threading.Tasks;
global using Domain.DomainModel.Summary;
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;

global using Infrastructure;
global using Infrastructure.DatabaseContext;
global using Infrastructure.Repositories;

global using BackgroundTasks.Tasks;

