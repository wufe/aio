using System;
using System.Collections.Generic;

namespace Aio.Presentation.Model {
    public class BadRequestOutputModel {

        public int Status { get; private set; } = 400;

        public IDictionary<string, string[]> Errors { get; private set; } = new Dictionary<string, string[]>();

        public static BadRequestOutputModel FromException (Exception exception) {
            return new BadRequestOutputModel() {
                Errors = new Dictionary<string, string[]>() {
                    { "_Exception", new [] { exception.Message, exception.StackTrace }}
                }
            };
        }
    }
}