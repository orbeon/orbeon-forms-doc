# Orbeon Forms documentation: instructions for agents

- The documentation is published at https://doc.orbeon.com/.
- `SUMMARY.md` is the table of contents:
  - It is a good starting point when looking for something.
  - When a page is added or removed, `SUMMARY.md` must be updated.
- When referencing documentation in a response, include both a reference to the corresponding Markdown file in this
  repository and the link to the publicly available page on https://doc.orbeon.com/.
- Including images in Markdown:
  - For 2x (Retina) screenshots narrower than `1520px`, use `<figure><img src="…" alt="…" width="…"></figure>` with
    `width` set to `round(pixel_width / 2)` so they don't render oversized in the ~`760px` column (add
    `<figcaption><p>…</p></figcaption>` only when a visible caption is wanted).
  - For full-width screenshots (`≥ 1520px` at 2x), 1x images, and inline/table images, use standard `![alt](path)`
    Markdown unless a visible `<figcaption>` is wanted.
