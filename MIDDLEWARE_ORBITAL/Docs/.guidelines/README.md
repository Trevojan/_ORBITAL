# Documentation Guide

This repository uses a two-tier documentation pattern:

## 📌 Quick Access (Root Level)

These files are in your home directory and act as **entry points and navigation hubs**:

- **`AGENTS.md`** – Quick index for agent-related documentation
- **`CLAUDE.md`** – Quick index for Claude-specific documentation

**Use these when:** You want a quick overview or to find what you're looking for.

## 📚 Comprehensive Guides (~/Docs/.guidelines/)

All **canonical, detailed documentation** lives here:

```
~/Docs/.guidelines/
├── agents.md                    # Main agent guide
├── claude.md                    # Main Claude guide
├── agent-architecture.md        # Deep dive: design patterns
├── agent-prompting.md          # Deep dive: instruction techniques
├── agent-examples.md           # Reference implementations
├── agent-troubleshooting.md    # Debugging & solutions
├── claude-capabilities.md      # What Claude can do
├── claude-api.md               # Technical integration
├── prompt-engineering.md       # Best practices
├── claude-limitations.md       # Known constraints
└── claude-versions.md          # Model history & details
```

**Use these when:** You need comprehensive, detailed information.

## 🔄 How the Redirect Works

### In AGENTS.md / CLAUDE.md

Each entry-point file contains:
- A brief description
- **A link to the main guideline** in `~/Docs/.guidelines/`
- A quick navigation menu
- File organization overview

**Example:**
```markdown
**For complete agent guidelines, see:** `~/Docs/.guidelines/agents.md`
```

### Reading Flow

```
User searches for "agents"
         ↓
Finds AGENTS.md (in root)
         ↓
Reads entry-point (quick overview)
         ↓
Clicks link → ~/Docs/.guidelines/agents.md
         ↓
Reads comprehensive guide
         ↓
Browses related .md files as needed
```

## 🚀 Quick Start

### I want to learn about agents:
```bash
cat AGENTS.md                              # Quick overview
cat ~/Docs/.guidelines/agents.md           # Full guide
cat ~/Docs/.guidelines/agent-examples.md   # See examples
```

### I want to use Claude's API:
```bash
cat CLAUDE.md                              # Quick overview
cat ~/Docs/.guidelines/claude-api.md       # Technical details
cat ~/Docs/.guidelines/prompt-engineering.md  # Best practices
```

### I need everything:
```bash
ls ~/Docs/.guidelines/
cat ~/Docs/.guidelines/*.md
```

## ✨ Why This Pattern?

| Benefit | Why It Matters |
|---------|----------------|
| **Discoverability** | Entry points in root are easy to find |
| **Organization** | All guidelines in one `.guidelines` folder |
| **Scalability** | Easy to add new topics (TOOLS.md, DEPLOYMENT.md, etc.) |
| **Single Source of Truth** | Guidelines don't live in multiple places |
| **Git-Friendly** | Easy to version, branch, and merge |
| **Reader-Friendly** | Quick-ref vs deep-dive clearly separated |

## 📝 Adding New Guidelines

To add a new topic, say "TOOLS.md":

1. **Create entry point:**
   ```bash
   # ~/TOOLS.md
   # (Brief intro, links to guideline folder)
   ```

2. **Create detailed guides:**
   ```bash
   ~/Docs/.guidelines/tools.md                # Main guide
   ~/Docs/.guidelines/tools-integration.md    # Specific topics
   ~/Docs/.guidelines/tools-examples.md       # Examples
   ```

3. **Update this README with the new entry point**

See STRUCTURE.md for templates and examples.

---

**Last Updated:** August 2026  
**Pattern:** Entry-Point with Canonical Hub  
**For Issues:** See the .guidelines folder for a CONTRIBUTING.md (if present)
