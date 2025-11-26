# Typepad Blog Migration Review Guide

## Overview

This guide helps you navigate the migrated TypePad blog content and find specific assets, media, and posts within each blog folder (AEC, AutoCAD, Manufacturing, etc.). Each blog has been converted to a structured format that makes it easy to locate original content, images, and related files.

## Folder Structure

Each blog folder (AEC, AutoCAD, Cloud_and_Mobile, etc.) contains:

```text
YourBlog/
├── Blog/                          # Main blog content
│   ├── _posts/                    # All your blog posts (Markdown files)
│   ├── _data/
│   │   ├── comments/              # Comment files (YAML format)
│   │   └── _assets/               # Downloaded images and files
├── parser_[blogname].log          # Migration log file
├── download_cache_[blogname].json # Download cache (technical)
└── [blogname]_export_summary.png  # Visual summary chart
```

## How to Find Content in Each Blog

### 1. Navigate to Your Blog Folder

1. Open the main repository folder
2. Select the blog you need (e.g., `AEC/`, `AutoCAD/`, `Manufacturing/`, etc.)
3. Each blog contains organized content ready for searching

### 2. Finding Blog Posts

**Location**: `Blog/_posts/`

- **File Format**: Each post is a `.md` (Markdown) file
- **Naming**: `YYYY-MM-DD-post-title.md`
- **Search Tips**:
  - Files are chronologically ordered by date
  - Use Ctrl+F to search by keywords in filenames
  - Post titles are in the filename after the date

**Each Post Contains**:
- Original post content
- Publication date and author
- Categories and tags
- Original TypePad URL for reference

### 3. Finding Images and Media Files

**Location**: `Blog/_data/_assets/`

**What You'll Find**:
- All images from blog posts (JPG, PNG, GIF)
- Documents and attachments (PDF, DOC, ZIP)
- Screenshots and diagrams
- Code samples and downloads

**How to Search for Assets**:

1. **By filename**: Assets often keep their original names
2. **By date**: Check the corresponding `.yml` file for upload dates
3. **By post**: Look in the post content for image references like `![alt text](filename.jpg)`
4. **Browse folder**: All assets are in one central location per blog

**Asset Metadata**: Each asset has a corresponding `.yml` file with original URL and download information.

### 4. Finding Comments and Discussions

**Location**: `Blog/_data/comments/`

- **Format**: YAML files (`.yml`)
- **Naming**: Matches the post filename (e.g., `2024-01-15-my-post.yml`)
- **Content**: All original comments with full context

**How to Find Comments for a Post**:

1. Find the post you're interested in (in `_posts/`)
2. Look for a matching `.yml` file in the `comments/` folder
3. Comments include author names, dates, and full discussion threads

### 5. Check the Summary Chart

**File**: `[blogname]_export_summary.png`

This visual chart shows:

- Total posts processed
- Total assets found and downloaded
- Download success rate
- Failure analysis (if any)

## Common Issues to Look For

### 1. Missing Images

- **Symptom**: Post references an image that doesn't exist in `_data_assets/`
- **Cause**: Image download failed during migration
- **Action**: Note which images are missing for potential manual recovery

### 2. Broken Links

- **Symptom**: Links in posts point to external sites that are now broken
- **Cause**: External websites have changed or disappeared
- **Action**: These are expected and not critical

### 3. Formatting Issues

- **Symptom**: Text formatting looks wrong in the Markdown
- **Cause**: HTML to Markdown conversion issues
- **Action**: Minor formatting can be fixed manually

## What Each File Type Contains

### Markdown Files (.md)

```text
---
layout: "post"
title: "Your Post Title"
date: "2024-01-15 10:30:00"
author: "Your Name"
categories: ["category1", "category2"]
original_url: "https://yourblog.typepad.com/post-url"
typepad_basename: "post-slug"
typepad_status: "Publish"
---

Your post content here...
```

### Comment Files (.yml)

```yaml
comments:
  - author: "Commenter Name"
    email: "commenter@example.com"
    date: "2024-01-15 11:00:00"
    body: "Comment text here..."
```

### Asset Files

- **Images**: `.jpg`, `.png`, `.gif`, etc.
- **Documents**: `.pdf`, `.doc`, `.docx`, etc.
- **Archives**: `.zip` files
- **Metadata**: `.yml` files describing each asset

### Overall Migration Summary

Your blog migration includes:

- **Posts**: All blog posts converted to Markdown
- **Comments**: All comments preserved in YAML format
- **Assets**: Images and files downloaded (success rate varies)
- **Structure**: Jekyll-ready format for easy deployment

## Next Steps

1. **Review the content** using this guide
2. **Note any issues** you find
3. **Test a few posts** to ensure they display correctly
4. **Check image references** to verify assets are properly linked

## Technical Notes

- **Success Rate**: Check the summary chart for download statistics
- **Failed Downloads**: Usually due to external links or server issues
- **File Organization**: All files are organized using Jekyll format
- **Metadata**: Original Typepad metadata is preserved

## Support

If you find issues or have questions:

1. Check the migration log file (`parser_[blogname].log`)
2. Review the summary chart for overall statistics
3. Contact Madhukar Moogala or Adam Nagy

---
