﻿using SemanticAnalyzer.Models;

namespace SemanticAnalyzer;

public interface ISemanticService
{
    /// <summary>
    /// Adding new parser with priority
    /// </summary>
    /// <param name="priority"></param>
    /// <typeparam name="T"></typeparam>
    void AddParser<T>(int priority)
        where T : class, IParser;

    /// <summary>
    /// Adding new parser with next priority
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void AddParser<T>()
        where T : class, IParser;

    /// <summary>
    /// Removing parser if exist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void RemoveParser<T>()
        where T : class, IParser;

    /// <summary>
    /// Parsing input line
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Return parsing context.</returns>
    ParsingContext ParseInputLine(string input);
}