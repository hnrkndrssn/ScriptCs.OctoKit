@echo off
scriptcs -cl
del scriptcs_packages.config
scriptcs -i ScriptCs.Octokit
scriptcs sample.csx
