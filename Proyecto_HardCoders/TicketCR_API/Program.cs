var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configurar CORS
builder.Services.AddCors(optiones => {

    //AGREANDO EL QUIEN Y COMO ACCEDER A MIS APIS
    optiones.AddPolicy(name: "MyCorsPolicy",  //A las politicas se les pone un nombre a elegir 

        //Configuraciones
        policy =>
        {
            policy.AllowAnyOrigin();//www.page.com www.mypage.net etc
            policy.AllowAnyHeader(); //application/json application/xml application/text
            policy.AllowAnyMethod(); //GET, POST , PUT , DELETE

        });


});

// Configure the HTTP request pipeline.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}else
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();


//Para habilitar el CORS, tiene que ir antes del app.Run();
app.UseCors();

app.Run();
