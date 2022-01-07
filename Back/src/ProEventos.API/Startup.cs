using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.Contratos;
using ProEventos.Persistence;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using AutoMapper;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ProEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProEventosContext>(
                context => context.UseSqlServer(Configuration.GetConnectionString("Default"))
            );

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;// Requer que a senha tenha digito. Nesse caso colocamos = false.
                options.Password.RequireNonAlphanumeric = false;// Requer que a senha tenha Alphanumeric. Nesse caso colocamos = false.
                options.Password.RequireLowercase = false;// Requer que a senha tenha Lowercase . Nesse caso colocamos = false.
                options.Password.RequireUppercase = false;// Requer que a senha tenha Uppercase. Nesse caso colocamos = false.                
                options.Password.RequiredLength = 4;// Requer que a senha tenha no mínimo 4 digitos. Nesse caso colocamos = 4.
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<ProEventosContext>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

            //Faz a autenticação com a chave registrada no "AppSettings:ChaveToken", no arquivo "appsettings.Development.json"
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(Configuration.GetSection("AppSettings:ChaveToken").Value)),//Aqui descriptografa a senha, usando a mesma chave.
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
            );

            ////LE-SE: Toda vez que chamar uma controler/uma rota...
            ////Toda vez que chamar uma controler/uma rota vai requerer que o usuário esteja autenticado automaticamente.
            //services.AddMvc(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});

            //Controla a redundancia do retorno da serialização dos itens.
            services.AddControllers()
                    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))// Converte o Enum(palavra) no número e vice vérsa.
                    .AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;});

            //LE-SE: Dentro do meu dom�nio da minha aplica��o (AppDomain), no dom�nio corrente (CurrentDomain), procura para mim que est�erando de "Profile" (na pasta "Helpers")
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IEventoPersist, EventoPersist>();
            services.AddScoped<ILotePersist, LotePersist>();
            services.AddScoped<IUserPersist, UserPersist>();

            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });

                //Adiciona o botão "Authorize" no swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token que, pegará após fazer login com seu usuário e senha.
                                Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                //Complemento da autenticação do botão "Authorize" no swagger
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Resources")),
                RequestPath = new PathString("/Resources")
            });
            
            app.UseAuthentication(); //Informa que terá que ter autenticação.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();// Primeiro vc autentica (dá o crachá)
            app.UseAuthorization();// Depois vc autoriza entrar com a autenticação (com o crachá).

            app.UseCors(cors => cors.AllowAnyHeader()//LE-SE: Dado qq cabeçalho de requisição do meu http
                                    .AllowAnyMethod()// vinda de qq método, ou seja, get ,post, put, delete, patch...
                                    .AllowAnyOrigin());// vinda de qq origem.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
