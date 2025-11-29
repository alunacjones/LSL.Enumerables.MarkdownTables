using System;
using System.Globalization;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class DisposableBritishCultureInfo : IDisposable
{
    private bool _disposedValue;
    private readonly CultureInfo _oldCulture;

    public DisposableBritishCultureInfo()
    {
        _oldCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                CultureInfo.CurrentCulture = _oldCulture;
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}