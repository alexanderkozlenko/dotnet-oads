﻿using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace WebTools.MicrosoftOffice.AddinHost.Resources
{
    /// <summary>Represents a resource manager that provides convenient access to embedded resources at run time.</summary>
    internal static class EmbeddedResourceManager
    {
        private static readonly Assembly _assembly = typeof(Program).Assembly;
        private static readonly string _namespace = typeof(Program).Namespace;

        /// <summary>Returns the value of the specified embedded string resource.</summary>
        /// <param name="name">The name of the embedded resource to retrieve.</param>
        /// <returns>The value of the embedded resource.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">The specified embedded resource is not found.</exception>
        public static string GetString(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            using var resourceStream = _assembly.GetManifestResourceStream(_namespace + "." + name);

            if (resourceStream == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Strings.GetString("resource.undefined"), name));
            }

            var buffer = new byte[resourceStream.Length];

            resourceStream.Read(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(buffer);
        }
    }
}