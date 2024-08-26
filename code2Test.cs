using System;
using NUnit.Framework;
using Moq;
using AutoFixture;
using AutoFixture.AutoMoq;

[TestFixture]
public class ServicioTests
{
    private Mock<ILibroRepository> _libroRepositoryMock;
    private Mock<IAutorRepository> _autorRepositoryMock;
    private Mock<ICiudadRepository> _ciudadRepositoryMock;
    private IFixture _fixture;
    private Servicio _sut;

    [SetUp]
    public void SetUp()
    {
        //inicializacion del mockup, mock de los repositorios utilizados
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _libroRepositoryMock = _fixture.Freeze<Mock<ILibroRepository>>();
        _autorRepositoryMock = _fixture.Freeze<Mock<IAutorRepository>>();
        _ciudadRepositoryMock = _fixture.Freeze<Mock<ICiudadRepository>>();
        _sut = new Servicio(_libroRepositoryMock.Object, _autorRepositoryMock.Object, _ciudadRepositoryMock.Object);
    }

    [Test]
    public async Task ShouldReturnExpectedResult() //primer test cuando encuentra resultados
    {
        var libros = _fixture.CreateMany<Servicio.Libro>(5).ToList(); //creo 5 libros
        var autores = _fixture.Build<Servicio.AutorLibro>().With(al => al.AutorId, libros.First().AutorId).CreateMany(3).ToList(); //creo 3 autores
    
        _libroRepositoryMock.Setup(repo => repo.ObtenerLibrosAsync()).ReturnsAsync(libros); //encuentro los libros que cree anteriormente
        _autorRepositoryMock.Setup(repo => repo.ObtenerAutoresAsync()).ReturnsAsync(autores); //encuentro los autores que cree anteriormente

        var result = await _sut.ObtenerAutoresAsync(); //llamo el metodo ObtenerAutoresAsync del codigo en produccion

        Assert.NotNull(result); // me aseguro con el Assert de que no sea nulo el resultado
        Assert.AreEqual(3, result.Count()); // deberia de ser los 3 autores que cree anteriormente
        _libroRepositoryMock.Verify(repo => repo.ObtenerLibrosAsync(), Times.Once); //verifico que solamente se haya ejecutado el metodo una vez
        _autorRepositoryMock.Verify(repo => repo.ObtenerAutoresAsync(), Times.Once);
    }
    [Test]
    public async Task ShouldReturnEmptyList()
    {
        _libroRepositoryMock.Setup(repo => repo.ObtenerLibrosAsync()).ReturnsAsync(Enumerable.Empty<Libro>()); //encuentro nada porque no hay nada creado
        _autorRepositoryMock.Setup(repo => repo.ObtenerAutoresAsync()).ReturnsAsync(Enumerable.Empty<AutorLibro>()); // encuentro nada porque no hay nada creado

        var result = await _sut.ObtenerAutoresAsync(); //llamo el metodo ObtenerAutores del codigo en produccion

        Assert.NotNull(result); // me aseguro de que el resultado no sea nulo
        Assert.IsEmpty(result); // me aseguro de que el resultado este vacio
        _libroRepositoryMock.Verify(repo => repo.ObtenerLibrosAsync(), Times.Once); // verifico que solamente se haya ejecutado el metodo una vez
        _autorRepositoryMock.Verify(repo => repo.ObtenerAutoresAsync(), Times.Once);
    }
}