module rec H3
open System
open Fable.Core
open Fable.Core.JS

let [<Import("*","h3-js")>] h3: H3_js.IExports = jsNative
let [<Import("UNITS","h3-js")>] units : H3_js.UNITS = jsNative
let [<Import("CoordIJ","h3-js")>] coordIJ : H3_js.CoordIJ = jsNative
let [<Import("H3IndexInput","h3-js")>] h3IndexInput : H3_js.H3IndexInput = jsNative


module H3_js =

    type [<AllowNullLiteral>] IExports =
        /// <summary>Whether a given string represents a valid H3 index</summary>
        /// <param name="h3Index">- H3 index to check</param>
        abstract h3IsValid: h3Index: H3IndexInput -> bool
        /// <summary>Whether the given H3 index is a pentagon</summary>
        /// <param name="h3Index">- H3 index to check</param>
        abstract h3IsPentagon: h3Index: H3IndexInput -> bool
        /// <summary>Whether the given H3 index is in a Class III resolution (rotated versus
        /// the icosahedron and subject to shape distortion adding extra points on
        /// icosahedron edges, making them not true hexagons).</summary>
        /// <param name="h3Index">- H3 index to check</param>
        abstract h3IsResClassIII: h3Index: H3IndexInput -> bool
        /// <summary>Get the number of the base cell for a given H3 index</summary>
        /// <param name="h3Index">- H3 index to get the base cell for</param>
        abstract h3GetBaseCell: h3Index: H3IndexInput -> float
        /// <summary>Get the indices of all icosahedron faces intersected by a given H3 index</summary>
        /// <param name="h3Index">- H3 index to get faces for</param>
        abstract h3GetFaces: h3Index: H3IndexInput -> ResizeArray<float>
        /// <summary>Returns the resolution of an H3 index</summary>
        /// <param name="h3Index">- H3 index to get resolution</param>
        abstract h3GetResolution: h3Index: H3IndexInput -> float
        /// <summary>Get the hexagon containing a lat,lon point</summary>
        /// <param name="lat">- Latitude of point</param>
        /// <param name="lng">- Longtitude of point</param>
        /// <param name="res">- Resolution of hexagons to return</param>
        abstract geoToH3: lat: float * lng: float * res: float -> H3Index
        /// <summary>Get the lat,lon center of a given hexagon</summary>
        /// <param name="h3Index">- H3 index</param>
        abstract h3ToGeo: h3Index: H3IndexInput -> ResizeArray<float>
        /// <summary>Get the vertices of a given hexagon (or pentagon), as an array of [lat, lng]
        /// points. For pentagons and hexagons on the edge of an icosahedron face, this
        /// function may return up to 10 vertices.</summary>
        /// <param name="h3Index">- H3 index</param>
        /// <param name="formatAsGeoJson">- Whether to provide GeoJSON output: [lng, lat], closed loops</param>
        abstract h3ToGeoBoundary: h3Index: H3Index * ?formatAsGeoJson: bool -> ResizeArray<ResizeArray<float>>
        /// <summary>Get the parent of the given hexagon at a particular resolution</summary>
        /// <param name="h3Index">- H3 index to get parent for</param>
        /// <param name="res">- Resolution of hexagon to return</param>
        abstract h3ToParent: h3Index: H3IndexInput * res: float -> H3Index
        /// <summary>Get the children/descendents of the given hexagon at a particular resolution</summary>
        /// <param name="h3Index">- H3 index to get children for</param>
        /// <param name="res">- Resolution of hexagons to return</param>
        abstract h3ToChildren: h3Index: H3IndexInput * res: float -> ResizeArray<H3Index>
        /// <summary>Get the center child of the given hexagon at a particular resolution</summary>
        /// <param name="h3Index">- H3 index to get center child for</param>
        /// <param name="res">- Resolution of hexagon to return</param>
        abstract h3ToCenterChild: h3Index: H3IndexInput * res: float -> H3Index
        /// <summary>Get all hexagons in a k-ring around a given center. The order of the hexagons is undefined.</summary>
        /// <param name="h3Index">- H3 index of center hexagon</param>
        /// <param name="ringSize">- Radius of k-ring</param>
        abstract kRing: h3Index: H3IndexInput * ringSize: float -> ResizeArray<H3Index>
        /// <summary>Get all hexagons in a k-ring around a given center, in an array of arrays
        /// ordered by distance from the origin. The order of the hexagons within each ring is undefined.</summary>
        /// <param name="h3Index">- H3 index of center hexagon</param>
        /// <param name="ringSize">- Radius of k-ring</param>
        abstract kRingDistances: h3Index: H3IndexInput * ringSize: float -> ResizeArray<ResizeArray<H3Index>>
        /// <summary>Get all hexagons in a hollow hexagonal ring centered at origin with sides of a given length.
        /// Unlike kRing, this function will throw an error if there is a pentagon anywhere in the ring.</summary>
        /// <param name="h3Index">- H3 index of center hexagon</param>
        /// <param name="ringSize">- Radius of ring</param>
        abstract hexRing: h3Index: H3IndexInput * ringSize: float -> ResizeArray<H3Index>
        /// <summary>Get all hexagons with centers contained in a given polygon. The polygon
        /// is specified with GeoJson semantics as an array of loops. Each loop is
        /// an array of [lat, lng] pairs (or [lng, lat] if isGeoJson is specified).
        /// The first loop is the perimeter of the polygon, and subsequent loops are
        /// expected to be holes.</summary>
        /// <param name="coordinates">- Array of loops, or a single loop</param>
        /// <param name="res">- Resolution of hexagons to return</param>
        /// <param name="isGeoJson">- Whether to expect GeoJson-style [lng, lat]
        ///                pairs instead of [lat, lng]</param>
        abstract polyfill: coordinates: U2<ResizeArray<ResizeArray<float>>, ResizeArray<ResizeArray<ResizeArray<float>>>> * res: float * ?isGeoJson: bool -> ResizeArray<H3Index>
        /// <summary>Get the outlines of a set of H3 hexagons, returned in GeoJSON MultiPolygon
        /// format (an array of polygons, each with an array of loops, each an array of
        /// coordinates). Coordinates are returned as [lat, lng] pairs unless GeoJSON
        /// is requested.
        /// 
        /// It is the responsibility of the caller to ensure that all hexagons in the
        /// set have the same resolution and that the set contains no duplicates. Behavior
        /// is undefined if duplicates or multiple resolutions are present, and the
        /// algorithm may produce unexpected or invalid polygons.</summary>
        /// <param name="h3Indexes">- H3 indexes to get outlines for</param>
        /// <param name="formatAsGeoJson">- Whether to provide GeoJSON output:
        ///            [lng, lat], closed loops</param>
        abstract h3SetToMultiPolygon: h3Indexes: ResizeArray<H3IndexInput> * ?formatAsGeoJson: bool -> ResizeArray<ResizeArray<ResizeArray<ResizeArray<float>>>>
        /// <summary>Compact a set of hexagons of the same resolution into a set of hexagons across
        /// multiple levels that represents the same area.</summary>
        /// <param name="h3Set">- H3 indexes to compact</param>
        abstract compact: h3Set: ResizeArray<H3IndexInput> -> ResizeArray<H3Index>
        /// <summary>Uncompact a compacted set of hexagons to hexagons of the same resolution</summary>
        /// <param name="compactedSet">- H3 indexes to uncompact</param>
        /// <param name="res">- The resolution to uncompact to</param>
        abstract uncompact: compactedSet: ResizeArray<H3IndexInput> * res: float -> ResizeArray<H3Index>
        /// <summary>Whether two H3 indexes are neighbors (share an edge)</summary>
        /// <param name="origin">- Origin hexagon index</param>
        /// <param name="destination">- Destination hexagon index</param>
        abstract h3IndexesAreNeighbors: origin: H3IndexInput * destination: H3IndexInput -> bool
        /// <summary>Get an H3 index representing a unidirectional edge for a given origin and destination</summary>
        /// <param name="origin">- Origin hexagon index</param>
        /// <param name="destination">- Destination hexagon index</param>
        abstract getH3UnidirectionalEdge: origin: H3IndexInput * destination: H3IndexInput -> H3Index
        /// <summary>Get the origin hexagon from an H3 index representing a unidirectional edge</summary>
        /// <param name="edgeIndex">- H3 index of the edge</param>
        abstract getOriginH3IndexFromUnidirectionalEdge: edgeIndex: H3IndexInput -> H3Index
        /// <summary>Get the destination hexagon from an H3 index representing a unidirectional edge</summary>
        /// <param name="edgeIndex">- H3 index of the edge</param>
        abstract getDestinationH3IndexFromUnidirectionalEdge: edgeIndex: H3IndexInput -> H3Index
        /// <summary>Whether the input is a valid unidirectional edge</summary>
        /// <param name="edgeIndex">- H3 index of the edge</param>
        abstract h3UnidirectionalEdgeIsValid: edgeIndex: H3IndexInput -> bool
        /// <summary>Get the [origin, destination] pair represented by a unidirectional edge</summary>
        /// <param name="edgeIndex">- H3 index of the edge</param>
        abstract getH3IndexesFromUnidirectionalEdge: edgeIndex: H3IndexInput -> ResizeArray<H3Index>
        /// <summary>Get all of the unidirectional edges with the given H3 index as the origin (i.e. an edge to
        /// every neighbor)</summary>
        /// <param name="h3Index">- H3 index of the origin hexagon</param>
        abstract getH3UnidirectionalEdgesFromHexagon: h3Index: H3IndexInput -> ResizeArray<H3Index>
        /// <summary>Get the vertices of a given edge as an array of [lat, lng] points. Note that for edges that
        /// cross the edge of an icosahedron face, this may return 3 coordinates.</summary>
        /// <param name="edgeIndex">- H3 index of the edge</param>
        /// <param name="formatAsGeoJson">- Whether to provide GeoJSON output: [lng, lat]</param>
        abstract getH3UnidirectionalEdgeBoundary: edgeIndex: H3IndexInput * ?formatAsGeoJson: bool -> ResizeArray<ResizeArray<float>>
        /// <summary>Get the grid distance between two hex indexes. This function may fail
        /// to find the distance between two indexes if they are very far apart or
        /// on opposite sides of a pentagon.</summary>
        /// <param name="origin">- Origin hexagon index</param>
        /// <param name="destination">- Destination hexagon index</param>
        abstract h3Distance: origin: H3IndexInput * destination: H3IndexInput -> float
        /// <summary>Given two H3 indexes, return the line of indexes between them (inclusive).
        /// 
        /// This function may fail to find the line between two indexes, for
        /// example if they are very far apart. It may also fail when finding
        /// distances for indexes on opposite sides of a pentagon.
        /// 
        /// Notes:
        /// 
        ///   - The specific output of this function should not be considered stable
        ///     across library versions. The only guarantees the library provides are
        ///     that the line length will be `h3Distance(start, end) + 1` and that
        ///     every index in the line will be a neighbor of the preceding index.
        ///   - Lines are drawn in grid space, and may not correspond exactly to either
        ///     Cartesian lines or great arcs.</summary>
        /// <param name="origin">- Origin hexagon index</param>
        /// <param name="destination">- Destination hexagon index</param>
        abstract h3Line: origin: H3IndexInput * destination: H3IndexInput -> ResizeArray<H3Index>
        /// <summary>Produces IJ coordinates for an H3 index anchored by an origin.
        /// 
        /// - The coordinate space used by this function may have deleted
        /// regions or warping due to pentagonal distortion.
        /// - Coordinates are only comparable if they come from the same
        /// origin index.
        /// - Failure may occur if the index is too far away from the origin
        /// or if the index is on the other side of a pentagon.
        /// - This function is experimental, and its output is not guaranteed
        /// to be compatible across different versions of H3.</summary>
        /// <param name="origin">- Origin H3 index</param>
        /// <param name="destination">- H3 index for which to find relative coordinates</param>
        abstract experimentalH3ToLocalIj: origin: H3IndexInput * destination: H3IndexInput -> CoordIJ
        /// <summary>Produces an H3 index for IJ coordinates anchored by an origin.
        /// 
        /// - The coordinate space used by this function may have deleted
        /// regions or warping due to pentagonal distortion.
        /// - Coordinates are only comparable if they come from the same
        /// origin index.
        /// - Failure may occur if the index is too far away from the origin
        /// or if the index is on the other side of a pentagon.
        /// - This function is experimental, and its output is not guaranteed
        /// to be compatible across different versions of H3.</summary>
        /// <param name="origin">- Origin H3 index</param>
        /// <param name="coords">- Coordinates as an `{i, j}` pair</param>
        abstract experimentalLocalIjToH3: origin: H3IndexInput * coords: CoordIJ -> H3Index
        /// <summary>Great circle distance between two geo points. This is not specific to H3,
        /// but is implemented in the library and provided here as a convenience.</summary>
        /// <param name="latlng1">- Origin coordinate as [lat, lng]</param>
        /// <param name="latlng2">- Destination coordinate as [lat, lng]</param>
        /// <param name="unit">- Distance unit (either UNITS.m or UNITS.km)</param>
        abstract pointDist: latlng1: ResizeArray<float> * latlng2: ResizeArray<float> * unit: string -> float
        /// <summary>Exact area of a given cell</summary>
        /// <param name="h3Index">- H3 index of the hexagon to measure</param>
        /// <param name="unit">- Distance unit (either UNITS.m2 or UNITS.km2)</param>
        abstract cellArea: h3Index: H3Index * unit: string -> float
        /// <summary>Exact length of a given unidirectional edge</summary>
        /// <param name="edge">- H3 index of the edge to measure</param>
        /// <param name="unit">- Distance unit (either UNITS.m, UNITS.km, or UNITS.rads)</param>
        abstract exactEdgeLength: edge: H3Index * unit: string -> float
        /// <summary>Average hexagon area at a given resolution</summary>
        /// <param name="res">- Hexagon resolution</param>
        /// <param name="unit">- Area unit (either UNITS.m2, UNITS.km2, or UNITS.rads2)</param>
        abstract hexArea: res: float * unit: string -> float
        /// <summary>Average hexagon edge length at a given resolution</summary>
        /// <param name="res">- Hexagon resolution</param>
        /// <param name="unit">- Distance unit (either UNITS.m, UNITS.km, or UNITS.rads)</param>
        abstract edgeLength: res: float * unit: string -> float
        /// <summary>The total count of hexagons in the world at a given resolution. Note that above
        /// resolution 8 the exact count cannot be represented in a JavaScript 32-bit number,
        /// so consumers should use caution when applying further operations to the output.</summary>
        /// <param name="res">- Hexagon resolution</param>
        abstract numHexagons: res: float -> float
        /// Get all H3 indexes at resolution 0. As every index at every resolution > 0 is
        /// the descendant of a res 0 index, this can be used with h3ToChildren to iterate
        /// over H3 indexes at any resolution.
        abstract getRes0Indexes: unit -> ResizeArray<H3Index>
        /// <summary>Get the twelve pentagon indexes at a given resolution.</summary>
        /// <param name="res">- Hexagon resolution</param>
        abstract getPentagonIndexes: res: float -> ResizeArray<H3Index>
        /// <summary>Convert degrees to radians</summary>
        /// <param name="deg">- Value in degrees</param>
        abstract degsToRads: deg: float -> float
        /// <summary>Convert radians to degrees</summary>
        /// <param name="rad">- Value in radians</param>
        abstract radsToDegs: rad: float -> float

    type H3Index =
        string

    type H3IndexInput =
        U2<string, ResizeArray<float>>

    type [<AllowNullLiteral>] CoordIJ =
        abstract i: float with get, set
        abstract j: float with get, set

    type [<AllowNullLiteral>] UNITS =
        abstract m: string with get, set
        abstract m2: string with get, set
        abstract km: string with get, set
        abstract km2: string with get, set
        abstract rads: string with get, set
        abstract rads2: string with get, set
