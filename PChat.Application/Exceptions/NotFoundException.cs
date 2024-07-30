using PChat.Domain.Dto;

namespace PChat.Application.Exceptions;

public class NotFoundException(string name, object key) : Exception($"The {name} with key: ({key}) was not found");