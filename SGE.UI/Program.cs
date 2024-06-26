using SGE.UI.Components;
using SGE.Aplicacion;
using SGE.Repositorios;
using SGE.Autenticacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<SGESqlite>();
SGESqlite.Inicializar();
builder.Services.AddDbContext<SGEcontext>();


// Caso de uso autenticar usuario
builder.Services.AddTransient<CasoDeUsoIniciarSesion>();
// Registro de dependencias necesarias para CasoDeUsoIniciarSesion
builder.Services.AddSingleton<IServicioSesion, ServicioSesion>();
builder.Services.AddScoped<IServicioIniciarSesion, ServicioIniciarSesion>();
//==================================================

//==================================================


// Casos de uso de Expediente
builder.Services.AddTransient<CasoDeUsoExpedienteAlta>();
builder.Services.AddTransient<CasoDeUsoExpedienteBaja>();
builder.Services.AddTransient<CasoDeUsoExpedienteModificacion>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaTodos>();
builder.Services.AddTransient<CasoDeUsoExpedienteConTramitesAsociados>();

// Casos de uso de Tramite
builder.Services.AddTransient<CasoDeUsoTramiteAlta>();
builder.Services.AddTransient<CasoDeUsoTramiteBaja>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaPorEtiqueta>();
builder.Services.AddTransient<CasoDeUsoTramiteModificacion>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaTodos>();

// Registro de dependencias de caso de uso de expedientes
builder.Services.AddScoped<IExpedienteRepositorio,RepositorioExpediente>();
builder.Services.AddScoped<IValidadorExpediente, ExpedienteValidador>();


// Registro de dependencias de caso de uso de tramites 
builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddScoped<IValidadorTramite, TramiteValidador>();
builder.Services.AddScoped<ServicioActualizacionEstado>();
builder.Services.AddScoped<EspecificacionCambioEstado>();
builder.Services.AddScoped<ITramiteRepositorio,RepositorioTramite>();


// Caso de usos de usuario: 
builder.Services.AddTransient<CasoDeUsoUsuarioAlta>();
builder.Services.AddTransient<CasoDeUsoUsuarioBaja>();
builder.Services.AddTransient<CasoDeUsoUsuarioModificacion>();
builder.Services.AddTransient<CasoDeUsoListarUsuarios>();
builder.Services.AddTransient<CasoDeUsoModificarPermisos>();
builder.Services.AddTransient<CasoDeUsoUsuarioConsultaPorId>();
// Registro de dependencias necesarias casos de uso de usuarios.
builder.Services.AddScoped<IUsuarioRepositorio,RepositorioUsuario>();
builder.Services.AddScoped<IValidadorUsuario, UsuarioValidador>();
builder.Services.AddScoped<IServicioHashContraseña, ServicioHashContraseña>();

//==================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
