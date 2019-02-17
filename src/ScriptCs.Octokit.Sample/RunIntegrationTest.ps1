scriptcs -cl
rm scriptcs_packages.config
scriptcs -i ScriptCs.Octokit -pre
scriptcs sample.csx
exit $LASTEXITCODE