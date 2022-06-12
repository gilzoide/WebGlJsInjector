# WebGL JS injector
Import JavaScript code as `<script>` tags in Unity WebGL builds' `index.html`
file.

This is a simpler alternative for HTML templates when you only need custom
`<script>` tags.

The new tags will be placed right before the last `<script>` tag found in the
HTML file, in the same order that they are found by `AssetDatabase`.


## Installing
Install via [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
using this repository URL:
```
https://github.com/gilzoide/WebGlJsInjector.git
```


## Usage
1. Create a file with the `.jstag` extension inside your `Assets` folder or UPM
   packages
2. (Optional) Add JavaScript code to this file, which will go inside the tag:
```html
<script>
Code will go here
</script>
```
3. (Optional) Add attributes in the import settings, which will go in the tag.
   Example of attributes are `type="module"` and `src="URL of some js lib in a CDN"`
```html
<script Attributes will go here>...</script>
```
4. Build for WebGL 🛠
5. (Optional) Open your `index.html` file in your code editor of choice to
   check that the expected `<script>` tags are there
6. Enjoy 🎉
