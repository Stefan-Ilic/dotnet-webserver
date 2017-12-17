using System.IO;
using BIF.SWE1.Interfaces;

namespace MyTests
{
    public interface IMyTests
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Returns a valid url for the ToLower plugin. The plugin should be able to return a page showing a posted text lowercase. The name of the posted field will be "text".
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetToLowerUrl();

        /// <summary>
        /// Must return a ToLower plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetToLowerPlugin();

        /// <summary>
        /// Must return a static file plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetStaticFilePlugin();
    }
}