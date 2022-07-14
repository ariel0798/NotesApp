using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.ApplicationCore.Notes.Queries.GetNoteIdByUserEmail;
using NotesApp.Domain.Errors.Exceptions;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public class NoteService : INoteService
{

}