﻿//
// Copyright (c) Ping Castle. All rights reserved.
// https://www.pingcastle.com
//
// Licensed under the Non-Profit OSL. See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Text;
using PingCastle.Rules;

namespace PingCastle.Healthcheck.Rules
{
	[RuleModel("S-NoPreAuthAdmin", RiskRuleCategory.StaleObjects, RiskModelCategory.ObjectConfig)]
	[RuleComputation(RuleComputationType.PerDiscover, 5)]
    [RuleDurANSSI(1, "kerberos_properties_preauth_priv", "Kerberos pre-authentication disabled for privileged accounts")]
    [RuleIntroducedIn(2,9)]
    public class HeatlcheckRuleStaledNoPreAuthAdmin : RuleBase<HealthcheckData>
    {
		protected override int? AnalyzeDataNew(HealthcheckData healthcheckData)
        {
            if (healthcheckData.UserAccountData.ListNoPreAuth != null)
            {
                foreach (var i in healthcheckData.UserAccountData.ListNoPreAuth)
                {
                    foreach (var j in healthcheckData.AllPrivilegedMembers)
                    {
                        if (i.DistinguishedName == j.DistinguishedName)
                        {
                            AddRawDetail(j.Name, j.Created, j.LastLogonTimestamp == DateTime.MinValue ? "Never" : j.LastLogonTimestamp.ToString("u"));
                            break;
                        }
                    }
                }
            }
            if (healthcheckData.ComputerAccountData.ListNoPreAuth != null)
            {
                foreach (var i in healthcheckData.ComputerAccountData.ListNoPreAuth)
                {
                    foreach (var j in healthcheckData.AllPrivilegedMembers)
                    {
                        if (i.DistinguishedName == j.DistinguishedName)
                        {
                            AddRawDetail(j.Name, j.Created, j.LastLogonTimestamp == DateTime.MinValue ? "Never" : j.LastLogonTimestamp.ToString("u"));
                            break;
                        }
                    }
                }
            }
            return null;
        }
    }
}
