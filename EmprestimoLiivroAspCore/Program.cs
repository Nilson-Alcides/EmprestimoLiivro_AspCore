using EmprestimoLiivroAspCore.Repository.Contrato;
using EmprestimoLiivroAspCore.Repository;
using EmprestimoLiivroAspCore.GerenciaArquivos;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

//Adicionar a interface como serviço 
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Corrigir problema com TEMPDATA para aumentar o tempo de duração
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing. 
    options.IdleTimeout = TimeSpan.FromSeconds(900);
    options.Cookie.HttpOnly = true;
    // Deixar informado para o navegador que a sessão é essencial
    options.Cookie.IsEssential = true;
});  
builder.Services.AddMvc().AddSessionStateTempDataProvider();

builder.Services.AddMemoryCache(); // Guardar os dados na memoria

//Add Gerenciador Arquivo como serviços
builder.Services.AddScoped<GerenciadorArquivo>();
builder.Services.AddScoped<EmprestimoLiivroAspCore.Cookie.Cookie>();
builder.Services.AddScoped<EmprestimoLiivroAspCore.CarrinhoCompra.CookieCarrinhoCompra>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
