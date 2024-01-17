﻿using System;
using System.IO;
using System.Reflection;
using Siren.Infrastructure.Mermaid;
using Siren.Infrastructure.Poco;

Console
	.WriteLine("Starting Siren console...");

if (args.Length < 2)
	throw new Exception("Expected 2 arguments");

var assemblyPath = args[0];
var outputPath = args[1];

if (string.IsNullOrEmpty(assemblyPath))
	throw new Exception("Assembly path argument invalid");

if (string.IsNullOrEmpty(outputPath))
	throw new Exception("Output path argument invalid");

Assembly assembly;

try
{
	assembly =
		Assembly
			.LoadFile(assemblyPath);
}
catch (FileNotFoundException)
{
	throw new Exception($"Could not load assembly from {assemblyPath}");
}

var universe =
	AssemblyScanner
		.Perform(assembly);

var result =
	MermaidRenderer
		.Perform(universe);

File
	.WriteAllText(
		outputPath,
		result
			.ToString()
	);