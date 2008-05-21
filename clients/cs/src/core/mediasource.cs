/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Net;
using System.Collections;
using System.IO;


#endregion

//////////////////////////////////////////////////////////////////////
// <summary>Contains the MediaSources currently implemented</summary>
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
   /// <summary>
    /// placeholder for a media object to be uploaded
    /// the base class only defines some primitives like content type
    /// </summary>
    public abstract class MediaSource 
    {
        private string contentType;
        private string contentName;

        /// <summary>
        /// constructs a media source based on a contenttype
        /// </summary>
        /// <param name="contenttype">the contenttype of the file</param>
        /// <returns></returns>
        public MediaSource(string contenttype)
        {
            this.ContentType = contenttype;
        }

        /// <summary>
        /// constructs a media source based on a contenttype and a name
        /// </summary>
        /// <param name="name">the name of the content</param>
        /// <param name="contenttype">the contenttype of the file</param>
        /// <returns></returns>
        public MediaSource(string name, string contenttype)
        {
            this.Name = name;
            this.ContentType = contenttype;
        }

        /// <summary>
        /// returns the length of the content of the media source
        /// </summary>
        /// <returns></returns>
        public abstract long ContentLength
        {
            get;
        }

        /// <summary>
        /// the name value of the content influence directly the slug
        /// header send
        /// </summary>
        /// <returns></returns>
        public string Name
        {
            get 
            {
                return this.contentName;
            }
            set
            {
                this.contentName = value;
            }
        }

        /// <summary>
        /// returns the contenttype of the media source, like img/jpg
        /// </summary>
        /// <returns></returns>
        public string ContentType
        {
            get 
            {
                return this.contentType;
            }
            set 
            {
                this.contentType = value;
            }
        }

        /// <summary>
        /// returns a stream of the actual content that is base64 encoded
        /// </summary>
        /// <returns></returns>
        public abstract Stream Data
        {
            get;
        }
    }



    /// <summary>
    /// a file based implementation. Takes a filename as it's base working mode
    /// </summary>
    /// <returns></returns>
    public class MediaFileSource : MediaSource
    {

        private string file;
        /// <summary>
        /// constructor. note that you can override the slug header without influencing the filename
        /// </summary>
        /// <param name="fileName">the file to be used, this will be the default slug header</param>
        /// <param name="contentType">the content type to be used</param>
        /// <returns></returns>
        public MediaFileSource(string fileName, string contentType) : base(fileName, contentType)
        {
            this.file = fileName;
        }

        /// <summary>
        /// returns the content lenght of the file
        /// </summary>
        /// <returns></returns>
        public override long ContentLength
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// returns the stream for the file. The file will be opened in readonly mode
        /// note, the caller has to release the resource
        /// </summary>
        /// <returns></returns>
        public override Stream Data
        {
            get
            {
                FileStream f = File.OpenRead(this.file);
                return f;
            }
        }
    }
}
/////////////////////////////////////////////////////////////////////////////
 