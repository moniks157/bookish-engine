using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Models;
using Shelfie.Domain.UseCases.Books.CreateBook;
using Shelfie.Domain.UseCases.Books.GetBook;

namespace Shelfie.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BookController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetBookQuery() { Id = id};
        var book = await _mediator.Send(query);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CreateBookModel createBookModel)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookModel);
        var book = await _mediator.Send(command);

        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }
}
