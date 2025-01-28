using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.InterfacesCU;
using LogicaDeAccesoDatos.Repositorios;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaDeAccesoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso.CUPais;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using LogicaAplicacion.InterfacesCU.InterfacesCUVuelo;
using LogicaAplicacion.CasosUso.CUCiudad;
using LogicaAplicacion.CasosUso.CUVuelo;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaAplicacion.CasosUso.CUGrupoDeViaje;
using LogicaAplicacion.CasosUso.CUHotel;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using LogicaAplicacion.InterfacesCU.InterfacesCUHotel;
using LogicaAplicacion.InterfacesCU.InterfacesCUToken;
using LogicaAplicacion.CasosUso.CUToken;
using WebApp;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using LogicaAplicacion.CasosUso.CUAerolinea;
using LogicaAplicacion.CasosUso.CUAeropuerto;
using LogicaAplicacion.InterfacesCU.InterfacesCUAeropuerto;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaAplicacion.CasosUso.CUItinerario;
using LogicaAplicacion.CasosUso.CUActividad;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaAplicacion.CasosUso.CUTraslado;
using LogicaAplicacion.InterfacesCU.InterfacesCUTraslado;
using LogicaAplicacion.InterfacesCU.InterfacesCUGrupoDeViaje;
using LogicaAplicacion.EnvioDeEmail;
using LogicaAplicacion;
using LogicaAplicacion.CasosUso;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

builder.Services.AddAuthentication(aut =>
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(aut =>
{
    aut.RequireHttpsMetadata = false;
    aut.SaveToken = true;
    aut.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    aut.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "No autorizado. Por favor, inicie sesi�n." });
            return context.Response.WriteAsync(result);
        }
    };
});

builder.Services.AddDistributedMemoryCache(); // Necesario para las sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de espera de la sesi�n
    options.Cookie.HttpOnly = true; // La cookie no es accesible por JavaScript
    options.Cookie.IsEssential = true; // La cookie es esencial para la aplicaci�n
});

builder.Services.AddControllers();
// Swagger/OpenAPI Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp API", Version = "v1" });

    // Definici�n del esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido de un espacio y el token JWT."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioPais, RepositorioPais>();
builder.Services.AddScoped<IRepositorioCiudad, RepositorioCiudad>();
builder.Services.AddScoped<IRepositorioGrupoDeViaje, RepositorioGrupoDeViaje>();
builder.Services.AddScoped<IRepositorioHotel, RepositorioHotel>();
builder.Services.AddScoped<IRepositorioToken, RepositorioToken>();
builder.Services.AddScoped<IRepositorioVuelo, RepositorioVuelo>();
builder.Services.AddScoped<IRepositorioAerolinea, RepositorioAerolinea>();
builder.Services.AddScoped<IRepositorioAeropuerto, RepositorioAeropuerto>();
builder.Services.AddScoped<IRepositorioItinerario, RepositorioItinerario>();
builder.Services.AddScoped<IRepositorioActividad, RepositorioActividad>();
builder.Services.AddScoped<IRepositorioTraslado, RepositorioTraslado>();




//USUARIOS
builder.Services.AddScoped<ILogin, CULogin>();
builder.Services.AddScoped<IAltaUsuario, CUAltaUsuario>();
builder.Services.AddScoped<ICambioContrasena, CUCambioConstrasena>();
builder.Services.AddScoped<IBuscarUsusarioPorId, CUBuscarUsuarioPorId>();
builder.Services.AddScoped<IModificarInfoPersonal, CuModificarInfoPersonal>();
builder.Services.AddScoped<IBuscarUsuarioPorPasaporte, CUBuscarUsuarioPorPasaporte>();
builder.Services.AddScoped<IActualizarEstadoCoordinador, CUActualizarEstadoCoordinador>();

//GRUPO DE VIAJE
builder.Services.AddScoped<IAltaGrupo, CUAltaGrupoDeViaje>();
builder.Services.AddScoped<IAgregarViajeroAGrupo, CUAgregarViajeroAGrupo>();
builder.Services.AddScoped<IEliminarViajero, CUEliminarViajero>();
builder.Services.AddScoped<IBuscarGrupoPorId, CUBuscarGrupoPorId>();
builder.Services.AddScoped<IBajaGrupoDeViaje, CUBajaGrupoDeViaje>();
builder.Services.AddScoped<IConfirmarGrupoDeViaje, CUConfirmarGrupoDeViaje>();



//CIUDAD
builder.Services.AddScoped<IObtenerCiudadesPorPais, CUObtenerCiudadPorPais>();
builder.Services.AddScoped<IListadoCiudades, CUListadoCiudades>();
builder.Services.AddScoped<IAltaCiudad, CUAltaCiudad>();

//PAIS

builder.Services.AddScoped<IListadoPaises, CUListadoPaises>();
builder.Services.AddScoped<IAltaPais, CUAltaPais>();
builder.Services.AddScoped<IObtenerPaises, CUObtenerPaises>();

//TOKEN
builder.Services.AddScoped<IAgregarTokenABlacklist, CUAgregarTokenABlacklist>();

//HOTEL
builder.Services.AddScoped<IGetGruposPorCoordinadorId, CUGetGruposPorCoordinadorId>();
builder.Services.AddScoped<IModificarHotel, CUModificarHotel>();
builder.Services.AddScoped<IBuscarHotelPorId, CUBuscarHotelPorId>();
builder.Services.AddScoped<IBajaHotel, CUBajaHotel>();
builder.Services.AddScoped<IFiltroHotelesPorPaisYCiudad, CUFiltroHotelesPorPaisYCiudad>();
builder.Services.AddScoped<IAltaHotel, CUAltaHotel>();
//VUELOS
builder.Services.AddScoped<IAltaVuelo, CUAltaVuelo>();
builder.Services.AddScoped<IBuscarVuelo, CUBuscarVuelo>();
builder.Services.AddScoped<IModificarVuelo, CUModificarVuelo>();
builder.Services.AddScoped<IBajaVuelo, CUBajaVuelo>();
builder.Services.AddScoped<IListadoFiltroVuelo, CUFiltroVuelosPorNombre>();


//AEROLINEA
builder.Services.AddScoped<IAltaAerolinea, CUAltaAerolinea>();
builder.Services.AddScoped<IBuscarAerolinea, CUBuscarAerolinea>();
builder.Services.AddScoped<IBajaAerolinea, CUBajaAerolinea>();
builder.Services.AddScoped<IModificarAerolinea, CUModificarAerolinea>();
builder.Services.AddScoped<IListadoFIltradoAerolinea, CUListadoFiltroAerolinea>();


//AEROPUERTO
builder.Services.AddScoped<IAltaAeropuerto, CUAltaAeropuerto>();
builder.Services.AddScoped<IBajaAeropuerto, CUBajaAeropuerto>();
builder.Services.AddScoped<IModificarAeropuerto, CUModificarAeropuerto>();
builder.Services.AddScoped<IListadoFiltradoAeropuerto, CUListadoFiltroAeropuertos>();
builder.Services.AddScoped<IBuscarAeropuerto, CUBuscarAeropuerto>();

//ITINERRIO
builder.Services.AddScoped<IAltaItinerario, CUAltaItinerario>();
builder.Services.AddScoped<IAgregarEventoAItinerario, CUAgregarEventoAItinerario>();
builder.Services.AddScoped<IEliminarEventoDeItinerario, CUEliminarEvento>();
builder.Services.AddScoped<IModificarEvento, CUModificarEventoDeItinerario>();
builder.Services.AddScoped<IEliminarItinerario, CUEliminarItinerario>();
builder.Services.AddScoped<IBuscarItinerario, CUBuscarItinerario>();
builder.Services.AddScoped<IListadoItinerarios, CUListadoItinerarios>();
builder.Services.AddScoped<IModificarGrupoDeItinerario, CUModificarGrupoDeItinerario>();
builder.Services.AddScoped<IObtenerEventosDelItinerario, CUObtenerEventosDelItinerario>();


//ACTIVIDAD
builder.Services.AddScoped<IAltaActividad, CUAltaActividad>();
builder.Services.AddScoped<IBajaActividad, CUBajaActividad>();
builder.Services.AddScoped<IBuscarActividad, CUBuscarActividad>();
builder.Services.AddScoped<IModificarActividad, CUModificarActividad>();
builder.Services.AddScoped<IListadoActividades, CUListadoActividad>();
builder.Services.AddScoped<IAltaDeUsuarioEnActividadOpcional, CUAltaDeUsuarioEnActividad>();


//TRASLADO
builder.Services.AddScoped<IAltaTraslado, CUAltaTraslado>();
builder.Services.AddScoped<IBajaTraslado, CUBajaTraslado>();
builder.Services.AddScoped<IBuscarTraslado, CUBuscarTraslado>();
builder.Services.AddScoped<IModificarTraslado, CUModificarTraslado>();
builder.Services.AddScoped<IListadoTraslados, CUListadoTraslado>();


builder.Services.AddTransient<IServicioEmail, ServicioEmail>();
builder.Services.AddScoped<IRecuperarContrasena, CURecuperarContrasena>();

ConfigurationBuilder confBuilder = new ConfigurationBuilder();
confBuilder.AddJsonFile("appsettings.json", false, true);
var config = confBuilder.Build();
string strCon = config.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<EContext>(options => options.UseSqlServer(strCon));


var app = builder.Build();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

// Middlewares
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
