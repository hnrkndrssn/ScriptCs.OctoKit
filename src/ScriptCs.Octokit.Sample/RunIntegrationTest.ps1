scriptcs -cl
rm scriptcs_packages.config
scriptcs -i ScriptCs.Octokit -P $args[0] -pre
scriptcs sample.csx
exit $LASTEXITCODE