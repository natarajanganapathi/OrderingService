global using System;
global using System.Net;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Data.Common;
global using System.Data.SqlClient;
global using MediatR;
global using Dapper;
global using FluentValidation;
global using Microsoft.Extensions.Logging;
global using System.Runtime.Serialization;
global using Serilog.Context;
global using Microsoft.EntityFrameworkCore;
global using System.Text.Json;
global using Azure.Core;
global using Azure.Identity;
global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using RabbitMQ.Client;
global using System.IO;
global using System.Reflection;
global using Microsoft.AspNetCore.Server.Kestrel.Core;
global using Serilog;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Azure.Messaging.ServiceBus;
global using Api.Api;
global using Api.Models.OrderModels;
global using Api.Commands.OrderCommands;
global using Api.Commands.ItemCommands;
global using Api.Commands.OrderItemMapCommands;
global using Api.Exceptions;
global using Api.Filters;
global using Api.Ententinos;
global using Api.ServiceBus;

global using Domain.DomainModel.OrderDomainModel.Entity;
global using Domain.DomainModel.OrderDomainModel;
global using Domain.DomainModel.Summary;

global using Infrastructure;
global using Infrastructure.DatabaseContext;
global using Infrastructure.Repositories;
