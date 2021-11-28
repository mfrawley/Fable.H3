import { getEnumerator, equals } from "./.fable/fable-library.3.2.9/Util.js";
import { printf, toConsole, interpolate, toText } from "./.fable/fable-library.3.2.9/String.js";
import * as h3_js from "h3-js";
import { ofArray } from "./.fable/fable-library.3.2.9/List.js";
import { Operators_FailurePattern } from "./.fable/fable-library.3.2.9/FSharp.Core.js";

export function assertEquals(v1, v2) {
    if (!equals(v1, v2)) {
        throw (new Error(toText(interpolate("%P() != %P()", [v1, v2]))));
    }
}

export function tests() {
    return ofArray([["numHexagons", (_arg1) => {
        const numHex = h3_js.numHexagons(5);
        assertEquals(numHex, 2016842);
    }], ["testGeoToH3", (_arg2) => {
        const nullIsland = h3_js.geoToH3(0, 0, 6);
        assertEquals(nullIsland, "86754e64fffffff");
    }]]);
}

(function (args) {
    const enumerator = getEnumerator(tests());
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const forLoopVar = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            const test = forLoopVar[1];
            const name = forLoopVar[0];
            try {
                test();
            }
            catch (matchValue) {
                const activePatternResult445 = Operators_FailurePattern(matchValue);
                if (activePatternResult445 != null) {
                    const msg = activePatternResult445;
                    toConsole(printf("%s: %s"))(name)(msg);
                }
                else {
                    throw matchValue;
                }
            }
        }
    }
    finally {
        enumerator.Dispose();
    }
    return 0;
})(typeof process === 'object' ? process.argv.slice(2) : []);

