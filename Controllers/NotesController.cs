﻿using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.NotesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly INotesRepository _noteRepository;

		public NotesController(INotesRepository noteRepository)
		{
			_noteRepository = noteRepository;
		}
		#region Get
		[HttpGet("GetNoteById/{id}")]
		public IActionResult GetNoteById(int id)
		{
			try
			{
				var note = _noteRepository.GetById(id);
				if (note == null)
				{
					return NotFound($"Note with ID {id} not found");
				}

				return Ok(note);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetNotesByUserId/{BookID}")]
		public IActionResult GetAllNotesForUser(int BookID)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var noteDTOs =_noteRepository.GetAllNotesForUser(userId,BookID);

				if (noteDTOs == null || noteDTOs.Count == 0)
				{
					return NotFound($"No notes found for user with ID {userId}");
				}

				return Ok(noteDTOs);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		#endregion
		#region Add
		[HttpPost("AddNote/{BookID}")]
		public IActionResult AddNote(int BookID,[FromBody] NoteDTO newNote)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Optionally, you can map the DTO to an entity if needed
					var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
					var noteEntity = new Notes
					{
						UserId = userId,
						BookId = BookID,
						PageNumber = newNote.PageNumber,
						NoteText = newNote.NoteText
					};

					_noteRepository.Add(noteEntity);
					return CreatedAtAction(nameof(GetNoteById), new { id = noteEntity.Id }, noteEntity);
				}
				else
					return BadRequest(ModelState);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
		#endregion
		#region Update
		[HttpPut("UpdateNote/{id}")]
		public IActionResult UpdateNote(int id, [FromBody] NoteDTO updatedNote)
		{
			try
			{
				if (ModelState.IsValid)
				{   var note = _noteRepository.GetById(id);
					if (note == null) return NotFound("This Note Doesnot Exsist");
					_noteRepository.UpdateNote(id, updatedNote);
					return Ok($"Note with ID {id} has been updated");
				}
				else { return BadRequest(ModelState); }
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
		#endregion
		[HttpDelete("DeleteNote/{id}")]
		public IActionResult DeleteNote(int id)
		{
			try
			{
				var note = _noteRepository.GetById(id);
				if (note == null) return NotFound("This Note Doesnot Exsist");
				_noteRepository.Delete(id);
				return Ok($"Note with ID {id} has been deleted");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
