using AutoMapper;
using meu.lms.api.Extensions;
using meu.lms.api.Hubs;
using meu.lms.api.Initializers;
using meu.lms.business.Containers.MicrosoftIoC;
using meu.lms.business.Interfaces;
using meu.lms.business.JwtConfiguration;
using meu.lms.dataaccess.Concrete.EntityFrameworkCore.Contexts;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace meu.lms.api
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDependencies();


            services.AddScoped<IJwtFactory, TokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<LmsDbContext>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
            //{
            //    opts.RequireHttpsMetadata = false;
            //    opts.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidIssuer = JwtInfo.Issuer,
            //        ValidAudience = JwtInfo.Audience,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey)),
            //        ValidateLifetime = true,
            //        ValidateAudience = true,
            //        ValidateIssuer = true,
            //        ValidateIssuerSigningKey = true,
            //        ClockSkew = TimeSpan.Zero
            //    };

            //});

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<LmsDbContext>().AddSignInManager<SignInManager<AppUser>>();

            services.AddIdentityServices(Configuration);


            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "MyTestService",
                    Version = "v1"

                });
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ICourseService _courseService, IAppUserService _appUserService, ITaskService _taskService, IAssignmentService _assignmentService)
        {

            app.UseCors(opts =>
            {
                opts.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            IdentityInitializer.SeedData(userManager, roleManager, _appUserService).Wait();
            CourseInitializer.SeedData(_courseService).Wait();
            TaskInitializer.SeedData(_taskService).Wait();
            AssignmentInitializer.SeedData(_assignmentService).Wait();


            //IdentityInitializer.SeedData(appUserService, appUserRoleService, appRoleService).Wait();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}"
                //);
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/MessageHub");
            });
        }
    }
}
