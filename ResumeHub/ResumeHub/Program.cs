using ResumeHub.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ResumeHub.DAL.IDbSessionDAL, ResumeHub.DAL.DbSessionDAL>();
builder.Services.AddSingleton<ResumeHub.DAL.IAuthDAL, ResumeHub.DAL.AuthDAL>();
builder.Services.AddSingleton<ResumeHub.DAL.IUserTokenDAL, ResumeHub.DAL.UserTokenDAL>();
builder.Services.AddSingleton<ResumeHub.DAL.IProfileDAL, ResumeHub.DAL.ProfileDAL>();
builder.Services.AddSingleton<ResumeHub.DAL.ISkillDAL, ResumeHub.DAL.SkillDAL>();
builder.Services.AddSingleton<ResumeHub.DAL.IPostDAL, ResumeHub.DAL.PostDAL>();

builder.Services.AddScoped<ResumeHub.BL.Auth.IAuth, ResumeHub.BL.Auth.Auth>();
builder.Services.AddSingleton<ResumeHub.BL.Auth.IEncrypt, ResumeHub.Deps.Encrypt>();
builder.Services.AddScoped<ResumeHub.BL.Auth.ICurrentUser, ResumeHub.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ResumeHub.BL.Auth.IDbSession, ResumeHub.BL.Auth.DbSession>();
builder.Services.AddScoped<ResumeHub.BL.General.IWebCookie, ResumeHub.Deps.WebCookie>();
builder.Services.AddSingleton<ResumeHub.BL.Profile.IProfile, ResumeHub.BL.Profile.Profile>();
builder.Services.AddSingleton<ResumeHub.BL.Resume.IResume, ResumeHub.BL.Resume.Resume>();
builder.Services.AddSingleton<ResumeHub.BL.Profile.ISkill, ResumeHub.BL.Profile.Skill>();
builder.Services.AddSingleton<ResumeHub.BL.Data.IPost, ResumeHub.BL.Data.Post>();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

ResumeHub.DAL.DbHelper.ConnString = app.Configuration["ConnectionStrings:Default"] ?? "";

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

