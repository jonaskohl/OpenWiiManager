using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class HashAlgorithmExtensions
    {
        public static async Task<byte[]> ComputeHashAsync(
            this HashAlgorithm hashAlgorithm, Stream stream,
            CancellationToken cancellationToken = default,
            IProgress<long> progress = null,
            int bufferSize = 1024 * 1024)
        {
            byte[] readAheadBuffer, buffer, hash;
            int readAheadBytesRead, bytesRead;
            long size, totalBytesRead = 0;
            size = stream.Length;
            readAheadBuffer = new byte[bufferSize];
            readAheadBytesRead = await stream.ReadAsync(readAheadBuffer, 0,
               readAheadBuffer.Length, cancellationToken);
            totalBytesRead += readAheadBytesRead;
            do
            {
                bytesRead = readAheadBytesRead;
                buffer = readAheadBuffer;
                readAheadBuffer = new byte[bufferSize];
                readAheadBytesRead = await stream.ReadAsync(readAheadBuffer, 0,
                    readAheadBuffer.Length, cancellationToken);
                totalBytesRead += readAheadBytesRead;

                if (readAheadBytesRead == 0)
                    hashAlgorithm.TransformFinalBlock(buffer, 0, bytesRead);
                else
                    hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                if (progress != null)
                    progress.Report(totalBytesRead);
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();
            } while (readAheadBytesRead != 0);
            return hash = hashAlgorithm.Hash;
        }
    }
}
